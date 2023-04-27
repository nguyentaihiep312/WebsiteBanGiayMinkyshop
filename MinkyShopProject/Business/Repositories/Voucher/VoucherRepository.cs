using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using Serilog;
using System.Net;

namespace MinkyShopProject.Business.Repositories.Voucher
{
    public class VoucherRepository : IVoucherRepository
	{
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public VoucherRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> AddAsync(VoucherCreateModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");

                var voucher = _mapper.Map<VoucherCreateModel, Entities.Voucher>(obj);
                voucher.NgayApDung = DateTime.Now;

                await _context.Voucher.AddAsync(voucher);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.Voucher, VoucherModel>(voucher);
                    return new ResponseObject<VoucherModel>(data, "Thêm thành công");
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
                var voucher = await _context.Voucher.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

                if (voucher == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                _context.Voucher.Remove(voucher);

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

        public async Task<Response> FindAsync(string id)
        {
            try
            {
                var voucher = await _context.Voucher.FirstOrDefaultAsync(c => c.Ma.ToLower().Trim() == id.ToLower().Trim());

                if (voucher != null)
                    return new ResponseObject<VoucherModel>(_mapper.Map<Entities.Voucher, VoucherModel>(voucher));
                return new ResponseError(HttpStatusCode.NotFound, "Không tìm thấy");
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> GetAsync(VoucherQueryModel obj)
        {
            try
            {
                return new ResponsePagination<VoucherModel>(_mapper.Map<Pagination<Entities.Voucher>, Pagination<VoucherModel>>(await _context.Voucher.Where(c => c.Ten.Contains(obj.Ten ?? "")).Where(c => c.TrangThai == obj.TrangThai || (c.TrangThai < 2 && obj.TrangThai == null)).AsQueryable().GetPageAsync(obj)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> UpdateAsync(Guid id, VoucherCreateModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Lỗi rồi");

                var voucher = _context.Voucher.AsNoTracking().FirstOrDefault(c => c.Id == id);

                if (voucher == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                voucher.MoTa = obj.MoTa;

                voucher.NgayKetThuc = obj.NgayKetThuc;

                voucher.SoLuong = obj.SoLuong;

                voucher.SoTienCan = obj.SoTienCan;

                voucher.SoTienGiam = obj.SoTienGiam;

                voucher.Ma = obj.Ma;

                voucher.Ten = voucher.Ten;

                voucher.TrangThai = obj.TrangThai;

                _context.Voucher.Update(_mapper.Map<VoucherCreateModel, Entities.Voucher>(obj));

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.Voucher, VoucherModel>(voucher);
                    return new ResponseObject<VoucherModel>(data, "Cập nhật thành công");
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
