using MinkyShopProject.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public class VoucherModel
    {
        public Guid Id { get; set; }

        public string? Ma { get; set; }

        public string? Ten { get; set; }

        public int LoaiGiamGia { get; set; }

        public int HinhThucGiamGia { get; set; }

        public float SoTienCan { get; set; }

        public float SoTienGiam { get; set; }

        public DateTime NgayApDung { get; set; } = DateTime.Now;

        public DateTime NgayKetThuc { get; set; }

        public int SoLuong { get; set; }

        public string? MoTa { get; set; }

        public int TrangThai { get; set; }

        public List<VoucherKhachHangModel>? VoucherKhachHangs { get; set; }

        public List<VoucherLogModel>? VoucherLogs { get; set; } = null!;
    }

    public class VoucherQueryModel : PaginationRequest
    {
        public int? TrangThai { get; set; }

        public string? Ten { get; set; }
    }

    public class VoucherCreateModel
    {
        public Guid Id { get; set; }

        public string? Ma { get; set; } = "VC" + Helper.RandomString(3);

        [Required(ErrorMessage = "Voucher Phải Có Tên")]
        public string? Ten { get; set; }

        public int LoaiGiamGia { get; set; }

        public int HinhThucGiamGia { get; set; }

        public float SoTienCan { get; set; }

        public float SoTienGiam { get; set; }

        public DateTime NgayApDung { get; set; }

        [MyDate(ErrorMessage = "Ngày Kết Thúc Phải Lớn Hơn Ngày Hiện Tại")]
        public DateTime NgayKetThuc { get; set; }

        [Range(1, 500, ErrorMessage = "Số lượng ít nhất là một")]
        public int SoLuong { get; set; }

        public string? MoTa { get; set; }

        public int TrangThai { get; set; }

        public List<VoucherKhachHangModel>? VoucherKhachHangs { get; set; }

        public List<VoucherLogModel>? VoucherLogs { get; set; } = null!;
    }

    public class VoucherKhachHangModel
    {
        public Guid Id { get; set; }

        public Guid IdVoucher { get; set; }

        public Guid IdKhachHang { get; set; }

        public int TrangThai { get; set; }

        public VoucherModel? Voucher { get; set; }

        public KhachHangModel? KhachHang { get; set; }
    }

    public class VoucherLogModel
    {
        public Guid Id { get; set; }

        public Guid IdHoaDon { get; set; }

        public Guid IdVoucher { get; set; }

        public float TienTruocKhiGiam { get; set; }

        public float TienSauKhiGiam { get; set; }

        public float SoTienGiam { get; set; }

        public DateTime NgayTao { get; set; }

        public HoaDonModel? HoaDon { get; set; }

        public VoucherModel? Voucher { get; set; }
    }
}
