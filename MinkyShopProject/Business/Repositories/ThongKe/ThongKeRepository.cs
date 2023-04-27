using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using Serilog;
using System.Net;

namespace MinkyShopProject.Business.Repositories.ThongKe
{
    public class ThongKeRepository : IThongKeRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public ThongKeRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> ThongKeSanPhamBanNhieuNhatTheoThangNam()
        {
            try
            {
                var result = await _context.HoaDon
                    .GroupBy(x => new { x.NgayTao.Year, x.NgayTao.Month })
                    .Select(g => new SoLuongThangNam
                    {
                        nam = g.Key.Year,
                        thang = g.Key.Month,
                        soluong = g.Count()
                    })
                    .ToListAsync();
                return new ResponseList<SoLuongThangNam>(result);
            }
            catch (Exception e)
            {
                Log.Error("Thống kê số lượng sản phẩm bán theo tháng không thành công");
                return new ResponseError(HttpStatusCode.InternalServerError,
                    "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }


        public async Task<Response> SanPhamBanNhieuNhatSoLuong(string loaiThongKe)
        {
            DateTime now = DateTime.Now.Date;
            DateTime start;
            DateTime end = now.AddDays(1).AddMilliseconds(-1);
            switch (loaiThongKe)
            {
                case "homnay":
                    start = now;
                    break;
                case "homqua":
                    start = now.AddDays(-1);
                    end = now.AddMilliseconds(-1);
                    break;
                case "7ngaytruoc":
                    start = now.AddDays(-6);
                    break;
                case "14ngaytruoc":
                    start = now.AddDays(-14);
                    end = start.AddDays(7).AddMilliseconds(-1);
                    break;
                case "thangtruoc":
                    start = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                    end = new DateTime(now.Year, now.Month, 1).AddMilliseconds(-1);
                    break;
                case "thangnay":
                    start = new DateTime(now.Year, now.Month, 1);
                    break;
                default:
                    return new ResponseError(HttpStatusCode.BadRequest, "Lựa chọn không hợp lệ.");
            }

            try
            {
                var ListSanPham = _context.HoaDon
                     .Where(x => x.NgayTao >= start && x.NgayTao <= end && x.TrangThai == 0)
                    .Include(po => po.HoaDonChiTiets)
                     .AsNoTracking()
                    .ToList()
                    .SelectMany(po => po.HoaDonChiTiets)
                    .GroupBy(s =>
                        _context.BienThe.Include(bt => bt.SanPham).FirstOrDefault(bt => bt.Id == s.IdBienThe).SanPham
                            .Ten)
                    .ToDictionary(g => g.Key, g => g.Sum(s => s.SoLuong));

                var result = ListSanPham
                    .Select(p => new SpBanNhieuNHatSoLuong()
                    {
                        Ten = p.Key,
                        SoLuong = p.Value
                    })
                    .OrderByDescending(p => p.SoLuong)
                    .Take(10)
                    .ToList();
                return new ResponseObject<List<SpBanNhieuNHatSoLuong>>(result);
            }
            catch (Exception e)
            {
                Log.Error("Thống kê tổng tiền theo ngày tháng năm không thành công");
                return new ResponseError(HttpStatusCode.InternalServerError,
                    "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }


        public async Task<Response> SanPhamBanNhieuNhatTien(string loaiThongKe)
        {
            DateTime now = DateTime.Now.Date;
            DateTime start;
            DateTime end = now.AddDays(1).AddMilliseconds(-1);
            switch (loaiThongKe)
            {
                case "homnay":
                    start = now;
                    break;
                case "homqua":
                    start = now.AddDays(-1);
                    end = now.AddMilliseconds(-1);
                    break;
                case "7ngaytruoc":
                    start = now.AddDays(-6);
                    break;
                case "14ngaytruoc":
                    start = now.AddDays(-7);
                    end = now.AddMilliseconds(-1).AddDays(-7);
                    break;
                case "thangtruoc":
                    start = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                    end = new DateTime(now.Year, now.Month, 1).AddMilliseconds(-1);
                    break;
                case "thangnay":
                    start = new DateTime(now.Year, now.Month, 1);
                    break;
                default:
                    return new ResponseError(HttpStatusCode.BadRequest, "Lựa chọn không hợp lệ.");
            }


            try
            {
                var ListSanPham = _context.HoaDon
                    .Where(x => x.NgayThanhToan >= start && x.NgayTao <= end && x.TrangThai == 0)
                    .Include(po => po.HoaDonChiTiets)
                    .ToList()
                    .SelectMany(po => po.HoaDonChiTiets)
                    .GroupBy(s =>
                        _context.BienThe.Include(bt => bt.SanPham).FirstOrDefault(bt => bt.Id == s.IdBienThe).SanPham
                            .Ten)
                    .ToDictionary(g => g.Key, g => g.Sum(s => s.SoLuong * s.DonGia));

                var result = ListSanPham
                    .Select(p => new SpBanNhieuNHatTien()
                    {
                        Ten = p.Key,
                        Tien = p.Value
                    })
                    .OrderByDescending(p => p.Tien)
                    .Take(10)
                    .ToList();
                return new ResponseObject<List<SpBanNhieuNHatTien>>(result);
            }
            catch (Exception e)
            {
                Log.Error("Thống kê tổng tiền theo ngày tháng năm không thành công");
                return new ResponseError(HttpStatusCode.InternalServerError,
                    "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }


        public async Task<Response> ThongKeTongTienNgayTienThangNam(string loaiThongKe)
        {
            DateTime now = DateTime.Now.Date;
            DateTime start;
            DateTime end = now.AddDays(1).AddMilliseconds(-1);
            switch (loaiThongKe)
            {
                case "homnay":
                    start = now;
                    break;
                case "homqua":
                    start = now.AddDays(-1);
                    end = now.AddMilliseconds(-1);
                    break;
                case "7ngaytruoc":
                    start = now.AddDays(-6);
                    break;
                case "14ngaytruoc":
                    start = now.AddDays(-14);
                    end = start.AddDays(7).AddMilliseconds(-1);
                    break;
                case "thangtruoc":
                    start = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                    end = new DateTime(now.Year, now.Month, 1).AddMilliseconds(-1);
                    break;
                case "thangnay":
                    start = new DateTime(now.Year, now.Month, 1);
                    break;
                default:
                    return new ResponseError(HttpStatusCode.BadRequest, "Lựa chọn không hợp lệ.");
            }

            try
            {
                var result = await _context.HoaDon
                    .Where(x => x.NgayHoanThanh >= start && x.NgayHoanThanh <= end && ((x.TrangThai == 0 && x.LoaiDonHang == 0 || x.LoaiDonHang == 1) || ((x.TrangThaiGiaoHang == 3 || x.TrangThaiGiaoHang == 5) && x.LoaiDonHang == 2)))
                    .GroupBy(x => new { x.NgayHoanThanh.Value.Day, x.NgayHoanThanh.Value.Month, x.NgayHoanThanh.Value.Year })
                    .Select(g => new TongTienNgayTienThangNam()
                    {
                        TongTien = g.Sum(x => x.TongTien),
                        ngay = g.Key.Day,
                        thang = g.Key.Month,
                        nam = g.Key.Year,
                    })
                    .ToListAsync();

                var tongTienTatCa = result.Sum(x => x.TongTien);
                var tongDonHuy = await _context.HoaDon.CountAsync(x => x.NgayTao >= start && x.NgayTao <= end && x.TrangThai == 1);
                var tongDonBan = await _context.HoaDon.CountAsync(x => x.NgayTao >= start && x.NgayTao <= end && x.TrangThai == 0);
                var tongDonQuay = await _context.HoaDon.CountAsync(x => x.NgayTao >= start && x.NgayTao <= end && x.LoaiDonHang == 0 && x.TrangThai == 0);
                var tongDonOnline = await _context.HoaDon.CountAsync(x => x.NgayTao >= start && x.NgayTao <= end && x.LoaiDonHang == 1 && x.TrangThai == 0);

                var thongKeTongTienResult = new ThongKeTongTienResult()
                {
                    TongTienTatCa = tongTienTatCa,
                    DonQuay = tongDonQuay,
                    DonOnline = tongDonOnline,
                    TongDonHuy = tongDonHuy,
                    TongDonBan = tongDonBan,
                    ThongKeTheoNgayTienThangNam = result
                };

                return new ResponseObject<ThongKeTongTienResult>(thongKeTongTienResult);

            }
            catch (Exception e)
            {
                Log.Error("Thống kê tổng tiền theo ngày tháng năm không thành công");
                return new ResponseError(HttpStatusCode.InternalServerError,
                    "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> ThongKeNhanVienBanDuocNhieuHoaDonvaTienNhat()
        {
            try
            {
                var result = await _context.NhanVien
                    .Include(x => x.HoaDons)
                    .Select(x => new NhanVienBanHangNhieuNHat()
                    {
                        Id = x.Id,
                        Ten = x.Ten,
                        sdt = x.Sdt,
                        ngayvaolam = x.NgayTao,
                        SoHoaDon = x.HoaDons.Count(hd => hd.TrangThai != 1),
                        TongTien = x.HoaDons.Where(hd => hd.TrangThai != 1).Sum(hd => hd.TongTien),
                    })
                    .OrderByDescending(x => x.TongTien)
                    .Take(10)
                    .ToListAsync();
                return new ResponseList<NhanVienBanHangNhieuNHat>(result);
            }
            catch (Exception e)
            {
                Log.Error("Thống kê số lượng nhân viên bán được nhiều hàng nhất không thành công");
                return new ResponseError(HttpStatusCode.InternalServerError,
                    "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }
        public async Task<Response> ThongKeNhanVienkhongBanDuocHang()
        {
            try
            {
                var result = await _context.NhanVien
                    .Include(x => x.HoaDons)
                    .Select(x => new NhanVienKhongBanHang()
                    {
                        Ten = x.Ten,
                        sdt = x.Sdt,
                        ngayvaolam = x.NgayTao,
                        TongTien = x.HoaDons.Where(hd => hd.TrangThai != 1).Sum(hd => hd.TongTien),
                        SoHoaDon = x.HoaDons.Count(hd => hd.TrangThai != 1),
                    })
                    .OrderBy(x => x.SoHoaDon)
                    .Take(10)
                    .ToListAsync();
                return new ResponseList<NhanVienKhongBanHang>(result);
            }
            catch (Exception e)
            {
                Log.Error("Thống kê số lượng nhân viên bán được nhiều hàng nhất không thành công");
                return new ResponseError(HttpStatusCode.InternalServerError,
                    "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> ThongKeKhackHangMuaNhieuNhat()
        {
            try
            {
                var result = await _context.KhachHang
                    .Include(x => x.HoaDons)
                    .Select(x => new KhachHangMuaNhieuNhat()
                    {
                        Ten = x.Ten,
                        sdt = x.Sdt,
                        SoHoaDon = x.HoaDons.Count(hd => hd.TrangThai == 0),
                        TongTien = x.HoaDons.Where(hd => hd.TrangThai == 0).Sum(hd => hd.TongTien),
                    })
                    .OrderByDescending(x => x.TongTien)
                    .Take(10)
                    .ToListAsync();
                return new ResponseList<KhachHangMuaNhieuNhat>(result);
            }
            catch (Exception e)
            {
                Log.Error("Thống kê số lượng nhân viên bán được nhiều hàng nhất không thành công");
                return new ResponseError(HttpStatusCode.InternalServerError,
                    "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }
        public async Task<Response> ThongKeKhachHangHuyHoaDonNhieuNhat()
        {
            try
            {
                var result = await _context.KhachHang
                    .Include(x => x.HoaDons)
                    .Select(x => new KhachHangHuyHoaDon()
                    {
                        Ten = x.Ten,
                        sdt = x.Sdt,
                        SoHoaDon = x.HoaDons.Count(hd => hd.TrangThai == 1),
                    })
                    .OrderByDescending(x => x.SoHoaDon)
                    .Take(10)
                    .ToListAsync();
                return new ResponseList<KhachHangHuyHoaDon>(result);
            }
            catch (Exception e)
            {
                Log.Error("Thống kê số lượng nhân viên bán được nhiều hàng nhất không thành công");
                return new ResponseError(HttpStatusCode.InternalServerError,
                    "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }
    }
}
