using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Repositories.ThongKe;
using MinkyShopProject.Common;

namespace MinkyShopProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThongKeController
    {
        private readonly IThongKeRepository _IThongKeRepository;

        public ThongKeController(IThongKeRepository IThongKeRepository)
        {
            _IThongKeRepository = IThongKeRepository;
        }

        [HttpGet, Route("ThongKeSanPhamBanNhieuNhatTheoThangNam")]
        public async Task<IActionResult> ThongKeSanPhamBanNhieuNhatTheoThangNam()
        {
            var result = await _IThongKeRepository.ThongKeSanPhamBanNhieuNhatTheoThangNam();
            return Helper.TransformData(result);
        }
        [HttpGet, Route("ThongKeNhanVienkhongBanDuocHang")]
        public async Task<IActionResult> ThongKeNhanVienkhongBanDuocHang()
        {
            var result = await _IThongKeRepository.ThongKeNhanVienkhongBanDuocHang();
            return Helper.TransformData(result);
        }

        [HttpGet, Route("ThongKeTongTienNgayTienThangNam")]
        public async Task<IActionResult> ThongKeTongTienNgayTienThangNam(string loaiThongKe)
        {
            var result = await _IThongKeRepository.ThongKeTongTienNgayTienThangNam(loaiThongKe);
            return Helper.TransformData(result);
        }

        [HttpGet, Route("ThongKeNhanVienBanDuocNhieuHoaDonvaTienNhat")]
        public async Task<IActionResult> ThongKeNhanVienBanDuocNhieuHoaDonvaTienNhat()
        {
            var result = await _IThongKeRepository.ThongKeNhanVienBanDuocNhieuHoaDonvaTienNhat();
            return Helper.TransformData(result);
        }
        [HttpGet, Route("SanPhamBanNhieuNhatSoLuong")]
        public async Task<IActionResult> SanPhamBanNhieuNhatSoLuong(string loaiThongKe)
        {
            var result = await _IThongKeRepository.SanPhamBanNhieuNhatSoLuong(loaiThongKe);
            return Helper.TransformData(result);
        }

        [HttpGet, Route("SanPhamBanNhieuNhatTien")]
        public async Task<IActionResult> SanPhamBanNhieuNhatTien(string loaiThongKe)
        {
            var result = await _IThongKeRepository.SanPhamBanNhieuNhatTien(loaiThongKe);
            return Helper.TransformData(result);
        }

        [HttpGet, Route("ThongKeKhackHangMuaNhieuNhat")]
        public async Task<IActionResult> ThongKeKhackHangMuaNhieuNhat()
        {
            var result = await _IThongKeRepository.ThongKeKhackHangMuaNhieuNhat();
            return Helper.TransformData(result);
        }
        [HttpGet, Route("ThongKeKhachHangHuyHoaDonNhieuNhat")]
        public async Task<IActionResult> ThongKeKhachHangHuyHoaDonNhieuNhat()
        {
            var result = await _IThongKeRepository.ThongKeKhachHangHuyHoaDonNhieuNhat();
            return Helper.TransformData(result);
        }
    }
}
