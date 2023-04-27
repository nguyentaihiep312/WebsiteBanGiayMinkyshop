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

namespace MinkyShopProject.Business.Repositories.NhomSanPham
{
    public class NhomSanPhamRepository : INhomSanPhamRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public NhomSanPhamRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> AddAsync(NhomSanPhamModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");

                var nhomSanPham = _mapper.Map<NhomSanPhamModel, Entities.NhomSanPham>(obj);

                await _context.NhomSanPham.AddAsync(nhomSanPham);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.NhomSanPham, NhomSanPhamModel>(nhomSanPham);
                    return new ResponseObject<NhomSanPhamModel>(data, "Thêm thành công");
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
                var nhomSanPham = await _context.NhomSanPham.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

                if (nhomSanPham == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                nhomSanPham.TrangThai = nhomSanPham.TrangThai == 0 ? 1 : 0;
                _context.NhomSanPham.Update(nhomSanPham);

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

        public async Task<Response> GetAsync(NhomSanPhamQueryModel obj)
        {
            try
            {
                return new ResponsePagination<NhomSanPhamModel>(_mapper.Map<Pagination<Entities.NhomSanPham>, Pagination<NhomSanPhamModel>>(await _context.NhomSanPham.Where(c => c.IdParent != null).Include(c => c.NhomSanPhams).Include(c => c.NhomSanPhamEntity).OrderByDescending(c => c.NgayTao).AsQueryable().GetPageAsync(obj)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> GetAsync2(NhomSanPhamQueryModel obj)
        {
            try
            {
                return new ResponsePagination<NhomSanPhamModel>(_mapper.Map<Pagination<Entities.NhomSanPham>, Pagination<NhomSanPhamModel>>(await _context.NhomSanPham.Where(c => c.IdParent == null).Include(c => c.NhomSanPhams).OrderByDescending(c => c.NgayTao).AsQueryable().GetPageAsync(obj)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> UpdateAsync(Guid id, NhomSanPhamModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Giá trị trả về không hợp lệ");

                var nhomSanPham = _context.NhomSanPham.AsNoTracking().FirstOrDefault(c => c.Id == id);

                if (nhomSanPham == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                nhomSanPham.Ten = obj.Ten;

                nhomSanPham.TrangThai = obj.TrangThai;

                nhomSanPham.IdParent = obj.IdParent;

                if (obj.NhomSanPhams != null && obj.NhomSanPhams.Count() > 0)
                {
                    nhomSanPham.NhomSanPhams = _mapper.Map<List<NhomSanPhamModel>, List<Entities.NhomSanPham>>(obj.NhomSanPhams);
                }

                _context.NhomSanPham.Update(_mapper.Map<NhomSanPhamModel, Entities.NhomSanPham>(obj));

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.NhomSanPham, NhomSanPhamModel>(nhomSanPham);
                    return new ResponseObject<NhomSanPhamModel>(data, "Cập nhật thành công");
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
