using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;
using Newtonsoft.Json;

namespace MinkyShopProject.Data.Models
{
    public class HoaDonModel
    {
        public Guid Id { get; set; }

        public Guid IdNhanVien { get; set; }

        public Guid? IdKhachHang { get; set; }

        public DateTime? NgayLayHang { get; set; }

        public string? Ma { get; set; }

        public string? GhiChuCapNhat { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public DateTime? NgayHoanThanh { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public DateTime? NgayThanhToan { get; set; }

        public DateTime? NgayXacNhan { get; set; }

        public DateTime? NgayGiaoHang { get; set; }

        public DateTime? NgayNhan { get; set; }

        public int LoaiDonHang { get; set; }

        public string? GhiChu { get; set; }

        public KhachHangModel? KhachHang { get; set; } = new KhachHangModel();

        public string? TenNguoiNhan { get; set; }

        public string? DiaChi { get; set; }

        public string? Sdt { get; set; }

        public float TienShip { get; set; }

        public float TongTien { get; set; }

        public DanhGiaModel? DanhGia { get; set; }

        public int TrangThaiGiaoHang { get; set; } // 0 : Giao Thành Công, 1 : Giao Thất Bại

        public string? GhiChuGiaoHang { get; set; }

        public NhanVienModel.NhanVienCreateModel? NhanVien { get; set; }

        public List<HinhThucThanhToanModel> HinhThucThanhToans { get; set; } = new List<HinhThucThanhToanModel>() { new HinhThucThanhToanModel() };

        public TrangThaiHoaDon TrangThai { get; set; }

        public List<VoucherLogModel>? VoucherLogs { get; set; }

        public List<HoaDonChiTietModel> HoaDonChiTiets { get; set; } = new List<HoaDonChiTietModel>();
    }

    public class HoaDonCreateModel
    {
        public Guid Id { get; set; }

        public Guid IdNhanVien { get; set; }

        public Guid? IdKhachHang { get; set; }

        public string? Ma { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public DateTime? NgayHoanThanh { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public DateTime? NgayThanhToan { get; set; }

        public string? GhiChuCapNhat { get; set; }

        public DateTime? NgayGiaoHang { get; set; }

        public DateTime? NgayNhan { get; set; }

        public DateTime? NgayXacNhan { get; set; }

        public DateTime? NgayLayHang { get; set; }

        public int LoaiDonHang { get; set; } = 0;

        public string? GhiChu { get; set; }

        public KhachHangModel? KhachHang { get; set; } = new KhachHangModel();

        public string? TenNguoiNhan { get; set; }

        public string? DiaChi { get; set; }

        public string? Sdt { get; set; }

        public float TienShip { get; set; }

        public float TongTien { get; set; }

        public int TrangThaiGiaoHang { get; set; } // 0 : Giao Thành Công, 1 : Giao Thất Bại

        public string? GhiChuGiaoHang { get; set; }

        public NhanVienModel.NhanVienCreateModel? NhanVien { get; set; }

        public List<HinhThucThanhToanModel> HinhThucThanhToans { get; set; } = new List<HinhThucThanhToanModel>() { new HinhThucThanhToanModel() };

        public TrangThaiHoaDon TrangThai { get; set; }

        public List<VoucherLogModel>? VoucherLogs { get; set; }

        public List<HoaDonChiTietModel> HoaDonChiTiets { get; set; } = new List<HoaDonChiTietModel>();
    }

    public class HinhThucThanhToanModel
    {
        public Guid Id { get; set; }

        public Guid IdHoaDon { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public int KieuThanhToan { get; set; }

        public float TongTienThanhToan { get; set; }

        public string? GhiChu { get; set; } = "";
    }

    public class HoaDonChiTietModel
    {
        public Guid Id { get; set; }

        public Guid IdBienThe { get; set; }

        public Guid IdHoaDon { get; set; }

        public int SoLuong { get; set; }

        public int SoLuongGoc { get; set; }

        public string? GhiChu { get; set; } = null!;

        public float DonGia { get; set; }

        public int TrangThai { get; set; }

        public BienTheModel? BienThe { get; set; }
    }

    public class HoaDonQueryModel : PaginationRequest
    {
        public int? TrangThai { get; set; }

        public string? Ma { get; set; } = "";

        public int? TrangThaiGiaoHang { get; set; }

        public int? LoaiHoaDon { get; set; }

        public Guid? IdKhachHang { get; set; }

        public DateTime? NgayTao { get; set; }
    }
}
