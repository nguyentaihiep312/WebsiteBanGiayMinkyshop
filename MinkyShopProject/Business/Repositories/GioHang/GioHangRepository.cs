using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.GioHang
{
    public class GioHangRepository : IGioHangRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public GioHangRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> AddAsync(GioHangModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");

                var gioHang = _mapper.Map<GioHangModel, Entities.GioHang>(obj);

                await _context.GioHang.AddAsync(gioHang);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.GioHang, GioHangModel>(gioHang);
                    return new ResponseObject<GioHangModel>(data, "Thêm thành công");
                }

                return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");
            }
            catch (Exception e)
            {
                Log.Error(e, string.Empty);
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            try
            {
                var gioHang = await _context.GioHang.AsNoTracking().FirstOrDefaultAsync(c => c.IdKhachHang == id);
                var gioHangChiTiet = await _context.GioHangChiTiet.AsNoTracking().FirstOrDefaultAsync(c => c.BienThe.Id == id);

                if (gioHangChiTiet != null)
                {
                    _context.GioHangChiTiet.Remove(gioHangChiTiet);
                }
                else
                {
                    if (gioHang != null)
                        _context.GioHang.Remove(gioHang);
                }

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    return new ResponseError(HttpStatusCode.OK, "Xóa thành công");
                }

                return new ResponseError(HttpStatusCode.BadRequest, "Xóa Thất Bại");
            }
            catch (Exception e)
            {
                Log.Error(e, string.Empty);
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> GetAsync(Guid id, GioHangQueryModel obj)
        {
            try
            {
                var result = await _context.GioHang.Include(c => c.GioHangChiTiets).ThenInclude(c => c.BienThe).ThenInclude(c => c.BienTheChiTiets).ThenInclude(c => c.GiaTri).FirstOrDefaultAsync(c => c.IdKhachHang == id);
                var gioHang = new GioHangModel();
                if (result != null)
                {
                    if (result.GioHangChiTiets != null)
                    {
                        foreach (var x in result.GioHangChiTiets)
                        {
                            var sp = await _context.SanPham.AsNoTracking().FirstOrDefaultAsync(c => c.Id == x.BienThe.IdSanPham);
                            if (sp != null)
                                x.BienThe.SanPham = sp;
                        }

                        gioHang = _mapper.Map<Entities.GioHang, GioHangModel>(result);

                        if (gioHang.GioHangChiTiets != null)
                        {

                            foreach (var x in gioHang.GioHangChiTiets)
                            {
                                var bienThe = await _context.BienThe.AsNoTracking().FirstOrDefaultAsync(c => c.Id == x.IdBienThe);
                                if (bienThe != null) x.SoLuongLucDau = bienThe.SoLuong;
                            }
                        }
                    }
                }
                return new ResponseObject<GioHangModel>(gioHang);
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> UpdateAsync(Guid id, GioHangModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Giá trị trả về không hợp lệ");

                if (obj.GioHangChiTiets != null)
                {
                    foreach (var x in obj.GioHangChiTiets)
                    {
                        x.BienThe = null;
                    }
                }

                var gioHang = _context.GioHang.AsNoTracking().FirstOrDefault(c => c.IdKhachHang == obj.IdKhachHang);

                if (gioHang == null)
                {
                    gioHang = new Entities.GioHang() { Id = Guid.NewGuid(), IdKhachHang = obj.IdKhachHang };
                    if (obj.GioHangChiTiets != null)
                    {
                        gioHang.GioHangChiTiets = _mapper.Map<List<GioHangChiTietModel>, List<GioHangChiTiet>>(obj.GioHangChiTiets);
                    }
                    await _context.GioHang.AddAsync(gioHang);
                }
                else
                {
                    gioHang = _mapper.Map<GioHangModel, Entities.GioHang>(obj);
                    _context.GioHang.Update(gioHang);
                }

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.GioHang, GioHangModel>(gioHang);
                    return new ResponseObject<GioHangModel>(data, "Cập nhật thành công");
                }

                return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
