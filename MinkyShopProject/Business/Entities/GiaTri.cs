using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class GiaTri
    {
        public Guid Id { get; set; }

        public Guid IdThuocTinh { get; set; }

        public string Ten { get; set; } = null!;

        public bool TrangThai { get; set; }

        public List<BienTheChiTiet> BienTheChiTiets { get; set; } = null!;

        public ThuocTinh ThuocTinh { get; set; } = null!;
    }
}
