using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class Voucher
    {
        public Guid Id { get; set; }

        public string? Ma { get; set; }

        public string? Ten { get; set; }

        public int LoaiGiamGia { get; set; }

        public int HinhThucGiamGia { get; set; }

        public float SoTienCan { get; set; }

        public float SoTienGiam { get; set; }

        public DateTime NgayApDung { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public int SoLuong { get; set; }

        public string? MoTa { get; set; }

        public int TrangThai { get; set; }

        public List<VoucherKhachHang> VoucherKhachHangs { get; set; } = null!;

        public List<VoucherLog>? VoucherLogs { get; set; } = null!;
    }
}
