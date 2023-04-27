using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class ThuocTinh
    {
        public Guid Id { get; set; }

        public string? Ten { get; set; }

        public int TrangThai { get; set; }

        public DateTime NgayTao { get; set; }

        public List<ThuocTinhSanPham> ThuocTinhSanPhams { get; set; } = null!;

        public List<GiaTri> GiaTris { get; set; } = null!;
    }
}
