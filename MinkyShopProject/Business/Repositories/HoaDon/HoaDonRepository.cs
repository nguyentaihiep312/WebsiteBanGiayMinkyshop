using AutoMapper;
using AutoMapper.Internal;
using Bogus.DataSets;
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

namespace MinkyShopProject.Business.Repositories.HoaDon
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public HoaDonRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> AddAsync(HoaDonCreateModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");

                obj.Ma = "HD" + Helper.RandomString(5);

                var hoaDon = _mapper.Map<HoaDonCreateModel, Entities.HoaDon>(obj);

                if (hoaDon.IdNhanVien == Guid.Empty)
                {
                    var nv = _context.giaoCas.FirstOrDefault();
                    if (nv != null)
                    {
                        hoaDon.IdNhanVien = nv.IdNhanVienTrongCa;
                    }
                    else
                    {
                        var nv2 = _context.NhanVien.FirstOrDefault();
                        if (nv2 != null)
                        {
                            hoaDon.IdNhanVien = nv2.Id;
                        }
                    }
                }

                if (hoaDon.IdKhachHang != null && hoaDon.IdKhachHang != Guid.Empty)
                {
                    var khachHang = _context.KhachHang.FirstOrDefault(c => c.Id == hoaDon.IdKhachHang);
                    if (khachHang != null)
                    {
                        khachHang.SoLanMua += 1;
                    }
                }


                if (hoaDon.VoucherLogs != null && hoaDon.VoucherLogs.Any())
                {
                    hoaDon.VoucherLogs[0].Voucher = null;
                    var voucher = _context.Voucher.FirstOrDefault(c => c.Id == hoaDon.VoucherLogs[0].IdVoucher);
                    if (voucher != null)
                        voucher.SoLuong -= 1;
                }

                await _context.HoaDon.AddAsync(hoaDon);

                foreach (var x in obj.HoaDonChiTiets)
                {
                    var bienThe = _context.BienThe.FirstOrDefault(c => c.Id == x.IdBienThe);
                    if (bienThe != null)
                        bienThe.SoLuong -= x.SoLuong;
                }

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.HoaDon, HoaDonModel>(hoaDon);
                    return new ResponseObject<HoaDonModel>(data, "Thêm thành công");
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
                var hoaDon = await _context.HoaDon.FirstOrDefaultAsync(c => c.Id == id);

                if (hoaDon == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                _context.HoaDon.Remove(hoaDon);

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
                var result = await _context.HoaDon.Include(c => c.VoucherLogs).ThenInclude(c => c.Voucher).Include(c => c.NhanVien).Include(c => c.KhachHang).Include(c => c.HinhThucThanhToans).Include(c => c.HoaDonChiTiets).ThenInclude(c => c.BienThe.BienTheChiTiets).ThenInclude(c => c.GiaTri).Include(c => c.DanhGia).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                if (result != null)
                {
                    foreach (var x in result.HoaDonChiTiets)
                    {
                        var sanPham = _context.SanPham.FirstOrDefault(c => c.Id == x.BienThe.IdSanPham);
                        if (sanPham != null)
                        {
                            x.BienThe.SanPham = sanPham;
                        }
                    }
                    return new ResponseObject<HoaDonModel>(_mapper.Map<Entities.HoaDon, HoaDonModel>(result));
                }
                return new ResponseError(HttpStatusCode.InternalServerError, "Không Tìm Thấy");
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> GetAsync(HoaDonQueryModel obj)
        {
            try
            {
                var result = await _context.HoaDon.Include(c => c.DanhGia).Include(c => c.VoucherLogs).ThenInclude(c => c.Voucher).Include(c => c.NhanVien).Include(c => c.KhachHang).Include(c => c.HinhThucThanhToans).Include(c => c.HoaDonChiTiets).ThenInclude(c => c.BienThe.BienTheChiTiets).ThenInclude(c => c.GiaTri).AsNoTracking().Where(c => c.LoaiDonHang == obj.LoaiHoaDon || (c.LoaiDonHang < 5 && obj.LoaiHoaDon == null)).Where(c => c.TrangThaiGiaoHang == obj.TrangThaiGiaoHang || (c.TrangThaiGiaoHang < 20 && obj.TrangThaiGiaoHang == null)).Where(c => c.TrangThai == obj.TrangThai || (c.TrangThaiGiaoHang < 20 && obj.TrangThai == null)).Where(c => c.Ma == obj.Ma || c.TenNguoiNhan.ToLower().Trim().Contains(!string.IsNullOrEmpty(obj.Ma) ? obj.Ma.ToLower().Trim() : "")).Where(c => (obj.IdKhachHang != null && c.IdKhachHang == obj.IdKhachHang) || c.LoaiDonHang < 5).Where(c => c.NgayTao >= obj.NgayTao).AsQueryable().GetPageAsync(obj);
                foreach (var x in result.Content)
                {
                    foreach (var y in x.HoaDonChiTiets)
                    {
                        var sanPham = _context.SanPham.FirstOrDefault(c => c.Id == y.BienThe.IdSanPham);
                        if (sanPham != null)
                        {
                            y.BienThe.SanPham = sanPham;
                        }
                    }
                }
                return new ResponsePagination<HoaDonModel>(_mapper.Map<Pagination<Entities.HoaDon>, Pagination<HoaDonModel>>(result));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> GetHoaDonByMaAsync(string ma)
        {
            try
            {
                return new ResponseObject<HoaDonModel>(_mapper.Map<Entities.HoaDon, HoaDonModel>(await _context.HoaDon.Include(c => c.VoucherLogs).ThenInclude(c => c.Voucher).Include(c => c.NhanVien).Include(c => c.KhachHang).Include(c => c.DanhGia).Include(c => c.HinhThucThanhToans).Include(c => c.HoaDonChiTiets).ThenInclude(c => c.BienThe.BienTheChiTiets).ThenInclude(c => c.GiaTri).AsNoTracking().FirstOrDefaultAsync(c => c.Ma == ma)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> GetHoaDonChuaHoanThanhAsync(HoaDonQueryModel obj)
        {
            try
            {
                return new ResponsePagination<HoaDonModel>(_mapper.Map<Pagination<Entities.HoaDon>, Pagination<HoaDonModel>>(await _context.HoaDon.Include(c => c.VoucherLogs).ThenInclude(c => c.Voucher).Include(c => c.NhanVien).Include(c => c.KhachHang).Include(c => c.HinhThucThanhToans).Include(c => c.HoaDonChiTiets).ThenInclude(c => c.BienThe.BienTheChiTiets).ThenInclude(c => c.GiaTri).Where(c => c.TrangThai == 3).AsNoTracking().AsQueryable().GetPageAsync(obj)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> GetHoaDonKhachHangAsync(Guid id)
        {
            try
            {
                return new ResponseList<HoaDonModel>(_mapper.Map<List<Entities.HoaDon>, List<HoaDonModel>>(await _context.HoaDon.Include(c => c.VoucherLogs).ThenInclude(c => c.Voucher).Include(c => c.NhanVien).Include(c => c.KhachHang).Include(c => c.DanhGia).Include(c => c.HinhThucThanhToans).Include(c => c.HoaDonChiTiets).ThenInclude(c => c.BienThe.BienTheChiTiets).ThenInclude(c => c.GiaTri).Where(c => c.IdKhachHang == id).AsNoTracking().ToListAsync()));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> UpdateAsync(Guid id, HoaDonCreateModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Giá trị trả về không hợp lệ");

                obj.NhanVien = null;

                var hoaDon = _context.HoaDon.AsNoTracking().Include(c => c.HoaDonChiTiets).AsNoTracking().FirstOrDefault(c => c.Id == id);

                if (hoaDon == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                hoaDon = _mapper.Map<HoaDonCreateModel, Entities.HoaDon>(obj);

                if (hoaDon.TrangThai == 1 || hoaDon.TrangThaiGiaoHang == 4)
                {
                    foreach (var x in hoaDon.HoaDonChiTiets)
                    {
                        var bienThe = await _context.BienThe.FirstOrDefaultAsync(c => c.Id == x.IdBienThe);
                        if (bienThe != null)
                        {
                            bienThe.SoLuong += x.SoLuong;
                        }
                    }
                }
                else
                {
                    if (hoaDon.TrangThaiGiaoHang == 5)
                    {
                        foreach (var x in hoaDon.HoaDonChiTiets)
                        {
                            var hoaDonChiTiet = _context.HoaDonChiTiet.AsNoTracking().FirstOrDefault(c => c.Id == x.Id);
                            var bienThe = await _context.BienThe.FirstOrDefaultAsync(c => c.Id == x.IdBienThe);
                            if (bienThe != null && x.TrangThai == 1)
                            {
                                if (hoaDonChiTiet != null && bienThe.SoLuong != hoaDonChiTiet.SoLuong)
                                {
                                    bienThe.SoLuong += hoaDonChiTiet.SoLuong;
                                }
                                bienThe.SoLuong += x.SoLuong;
                            }
                            else
                            {
                                if (bienThe != null && hoaDonChiTiet != null && x.TrangThai == 0 && hoaDonChiTiet.TrangThai == 1)
                                {
                                    if (bienThe.SoLuong != hoaDonChiTiet.SoLuong)
                                    {
                                        bienThe.SoLuong += hoaDonChiTiet.SoLuong;
                                    }
                                    bienThe.SoLuong -= x.SoLuong;
                                }
                            }
                        }
                    }
                    else
                    {
                        var hoaDonBanDau = _context.HoaDon.AsNoTracking().FirstOrDefault(c => c.Id == id);
                        if (hoaDonBanDau != null && hoaDonBanDau.TrangThai == 1)
                        {
                            foreach (var x in hoaDon.HoaDonChiTiets)
                            {
                                var hoaDonChiTiet = _context.HoaDonChiTiet.AsNoTracking().FirstOrDefault(c => c.Id == x.Id);
                                var bienThe = await _context.BienThe.FirstOrDefaultAsync(c => c.Id == x.IdBienThe);
                                if (bienThe != null && hoaDonChiTiet != null)
                                {
                                    bienThe.SoLuong -= x.SoLuong;
                                }
                            }
                        }

                        foreach (var x in hoaDon.HoaDonChiTiets)
                        {
                            var hoaDonChiTiet = _context.HoaDonChiTiet.AsNoTracking().FirstOrDefault(c => c.Id == x.Id);
                            var bienThe = await _context.BienThe.FirstOrDefaultAsync(c => c.Id == x.IdBienThe);
                            if (bienThe != null && hoaDonChiTiet != null)
                            {
                                if (bienThe.SoLuong != hoaDonChiTiet.SoLuong)
                                {
                                    bienThe.SoLuong += hoaDonChiTiet.SoLuong;
                                }
                                bienThe.SoLuong -= x.SoLuong;
                            }
                        }
                    }
                }

                hoaDon.VoucherLogs = new List<VoucherLog>();

                _context.HoaDon.Update(hoaDon);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.HoaDon, HoaDonModel>(hoaDon);
                    return new ResponseObject<HoaDonModel>(data, "Cập nhật thành công");
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
