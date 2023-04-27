using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using Serilog;
using System.Net;

namespace MinkyShopProject.Business.Repositories.ThuocTinh
{
    public class ThuocTinhRepository : IThuocTinhRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public ThuocTinhRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> AddAsync(ThuocTinhModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");

                var thuocTinh = _mapper.Map<ThuocTinhModel, Entities.ThuocTinh>(obj);

                await _context.ThuocTinh.AddAsync(thuocTinh);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.ThuocTinh, ThuocTinhModel>(thuocTinh);
                    return new ResponseObject<ThuocTinhModel>(data, "Thêm thành công");
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
                var giaTri = _context.GiaTri.AsNoTracking().FirstOrDefault(c => c.Id == id);

                if (giaTri != null)
                {
                    giaTri.TrangThai = !giaTri.TrangThai;
                    _context.GiaTri.Update(giaTri);
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

        public async Task<Response> GetAsync(ThuocTinhQueryModel obj)
        {
            try
            {
                return new ResponsePagination<ThuocTinhModel>(_mapper.Map<Pagination<Entities.ThuocTinh>, Pagination<ThuocTinhModel>>(await _context.ThuocTinh.Include(c => c.GiaTris.OrderBy(c => c.Ten)).Where(c => c.Ten.Contains(obj.Ten ?? "")).Where(c => c.TrangThai == obj.TrangThai || (c.TrangThai < 2 && obj.TrangThai == null)).AsQueryable().GetPageAsync(obj)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> UpdateAsync(Guid id, ThuocTinhModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Lỗi rồi");

                var thuocTinh = _context.ThuocTinh.AsNoTracking().FirstOrDefault(c => c.Id == id);

                if (thuocTinh == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                thuocTinh.Ten = obj.Ten;

                thuocTinh.TrangThai = obj.TrangThai;

                if (obj.GiaTris != null)
                {
                    thuocTinh.GiaTris = _mapper.Map<List<GiaTriModel>, List<Entities.GiaTri>>(obj.GiaTris);
                }

                _context.ThuocTinh.Update(_mapper.Map<ThuocTinhModel, Entities.ThuocTinh>(obj));

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.ThuocTinh, ThuocTinhModel>(thuocTinh);
                    return new ResponseObject<ThuocTinhModel>(data, "Cập nhật thành công");
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
