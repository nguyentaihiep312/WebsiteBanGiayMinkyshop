using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class ThuocTinhSanPham
    {
        public Guid Id { get; set; }

        public Guid IdThuocTinh { get; set; }

        public Guid IdSanPham { get; set; }

        public SanPham SanPham { get; set; } = null!;

        public ThuocTinh ThuocTinh { get; set; } = null!;

        public List<BienTheChiTiet> BienTheChiTiets { get; set; } = null!;
    }
}
