using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class HoaDonChiTiet
    {
        public Guid Id { get; set; }

        public Guid IdBienThe { get; set; }

        public Guid IdHoaDon { get; set; }

        public int SoLuong { get; set; }

        public string? GhiChu { get; set; } = null!;

        public int TrangThai { get; set; }

        public float DonGia { get; set; }

        public HoaDon HoaDon { get; set; } = null!;

        public BienThe BienThe { get; set; } = null!;
    }
}
