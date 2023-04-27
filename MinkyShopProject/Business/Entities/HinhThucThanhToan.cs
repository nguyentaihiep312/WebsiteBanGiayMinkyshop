using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class HinhThucThanhToan
    {
        public Guid Id { get; set; }

        public Guid IdHoaDon { get; set; }

        public DateTime NgayTao { get; set; }

        public int KieuThanhToan { get; set; }

        public float TongTienThanhToan { get; set; }

        public string GhiChu { get; set; } = null!;

        public HoaDon HoaDon { get; set; } = null!;
    }
}
