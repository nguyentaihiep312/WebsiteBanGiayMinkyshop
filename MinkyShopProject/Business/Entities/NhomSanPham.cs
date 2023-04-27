using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class NhomSanPham
    {
        public Guid Id { get; set; }

        public Guid? IdParent { get; set; }

        public string? Ten { get; set; }

        public int TrangThai { get; set; }

        public DateTime NgayTao { get; set; }

        public List<NhomSanPham> NhomSanPhams { get; set; } = null!;

        public NhomSanPham NhomSanPhamEntity { get; set; } = null!;

        public List<SanPham> SanPhams { get; set; } = null!;
    }
}
