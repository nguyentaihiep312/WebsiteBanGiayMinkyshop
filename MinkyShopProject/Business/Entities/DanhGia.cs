using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class DanhGia
    {
        public Guid Id { get; set; }

        public Guid IdKhachHang { get; set; }

        public Guid IdSanPham { get; set; }

        public Guid IdHoaDon { get; set; }

        public int SoDanhGia { get; set; }

        public DateTime NgayDanhGia { get; set; }

        public string? Anh { get; set; } = null!;

        public string NoiDung { get; set; } = null!;

        public int TrangThai { get; set; }

        public KhachHang? KhachHang { get; set; } = null!;

        public HoaDon? HoaDon { get; set; } = null!;
    }
}
