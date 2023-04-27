using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;
using Serilog;
using System.Net;
using System.Xml.Linq;

namespace MinkyShopProject.Business.Repositories.SanPham
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public SanPhamRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            try
            {
                var sanPham = await _context.SanPham.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

                if (sanPham == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                _context.SanPham.Remove(sanPham);

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
                return new ResponseObject<SanPhamModel>(_mapper.Map<Entities.SanPham, SanPhamModel>(await _context.SanPham.AsNoTracking().Include(c => c.NhomSanPham).Include(c => c.BienThes).ThenInclude(c => c.BienTheChiTiets).ThenInclude(c => c.GiaTri).FirstAsync(c => c.Id == id)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> GetAsync(SanPhamQueryModel obj)
        {
            try
            {
                return new ResponsePagination<SanPhamModel>(_mapper.Map<Pagination<Entities.SanPham>, Pagination<SanPhamModel>>(await _context.SanPham.Include(c => c.NhomSanPham).ThenInclude(c => c.NhomSanPhamEntity).Include(c => c.BienThes).ThenInclude(c => c.BienTheChiTiets).ThenInclude(c => c.GiaTri).AsNoTracking().Where(c => c.IdNhomSanPham == obj.IdNhomSanPham || (c.Ten.Contains("") && obj.IdNhomSanPham == Guid.Empty)).Where(c => c.Ma == obj.Ten || c.Ten.ToLower().Trim().Contains(obj.Ten ?? "")).AsQueryable().GetPageAsync(obj)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> UpdateAsync(Guid id, SanPhamModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Giá trị trả về không hợp lệ");

                var sanPham = _context.SanPham.AsNoTracking().FirstOrDefault(c => c.Id == id);

                if (sanPham == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                sanPham.Ten = obj.Ten;

                sanPham.Anh = obj.Anh;

                sanPham.MoTa = obj.MoTa;

                sanPham.TrangThai = obj.TrangThai;

                sanPham.IdNhomSanPham = obj.IdNhomSanPham;

                if (obj.BienThes != null)
                {
                    sanPham.BienThes = _mapper.Map<List<BienTheModel>, List<Entities.BienThe>>(obj.BienThes);
                }

                _context.SanPham.Update(sanPham);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.SanPham, SanPhamModel>(sanPham);
                    return new ResponseObject<SanPhamModel>(obj, "Cập nhật thành công");
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
