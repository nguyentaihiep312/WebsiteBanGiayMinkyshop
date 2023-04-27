using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using Serilog;
using System.Net;

namespace MinkyShopProject.Business.Repositories.DanhGia
{
    public class DanhGiaRepository : IDanhGiaRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public DanhGiaRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> AddAsync(DanhGiaCreateModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");

                var danhGia = _mapper.Map<DanhGiaCreateModel, Entities.DanhGia>(obj);

                danhGia.HoaDon = null;
                danhGia.KhachHang = null;

                await _context.DanhGia.AddAsync(danhGia);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.DanhGia, DanhGiaModel>(danhGia);
                    return new ResponseObject<DanhGiaModel>(data, "Thêm thành công");
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
                var danhGia = await _context.DanhGia.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

                if (danhGia == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                _context.DanhGia.Remove(danhGia);

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

        public async Task<Response> FindAsync(Guid id)
        {
            try
            {
                var danhGia = await _context.DanhGia.Include(c => c.HoaDon).ThenInclude(c => c.HoaDonChiTiets).ThenInclude(c => c.BienThe).ThenInclude(c => c.SanPham).Where(c => c.HoaDon.HoaDonChiTiets.Any(c => c.BienThe.SanPham.Id == id)).AsNoTracking().ToListAsync();

                if (danhGia != null)
                    return new ResponseList<DanhGiaModel>(_mapper.Map<List<Entities.DanhGia>, List<DanhGiaModel>>(danhGia));
                return new ResponseError(HttpStatusCode.NotFound, "Không tìm thấy");
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> GetAsync(DanhGiaQueryModel obj)
        {
            try
            {
                return new ResponsePagination<DanhGiaModel>(_mapper.Map<Pagination<Entities.DanhGia>, Pagination<DanhGiaModel>>(await _context.DanhGia.AsQueryable().GetPageAsync(obj)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> UpdateAsync(Guid id, DanhGiaCreateModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Lỗi rồi");

                var danhGia = _context.DanhGia.AsNoTracking().FirstOrDefault(c => c.Id == id);

                if (danhGia == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                danhGia.Anh = obj.Anh;

                danhGia.SoDanhGia = obj.SoDanhGia;

                danhGia.NgayDanhGia = obj.NgayDanhGia;

                danhGia.IdKhachHang = obj.IdKhachHang;

                danhGia.IdHoaDon = obj.IdHoaDon;

                danhGia.NoiDung = obj.NoiDung;

                danhGia.TrangThai = obj.TrangThai;

                _context.DanhGia.Update(_mapper.Map<DanhGiaCreateModel, Entities.DanhGia>(obj));

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.DanhGia, DanhGiaModel>(danhGia);
                    return new ResponseObject<DanhGiaModel>(data, "Cập nhật thành công");
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
