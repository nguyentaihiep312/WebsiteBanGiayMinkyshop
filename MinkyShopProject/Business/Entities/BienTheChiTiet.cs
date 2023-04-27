using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class BienTheChiTiet
    {
        public Guid Id { get; set; }

        public Guid IdThuocTinhSanPham { get; set; }

        public Guid IdBienThe { get; set; }

        public Guid IdGiaTri { get; set; }

        public int TrangThai { get; set; }

        public ThuocTinhSanPham ThuocTinhSanPham { get; set; } = null!;

        public BienThe BienThe { get; set; } = null!;

        public GiaTri GiaTri { get; set; } = null!;
    }
}
