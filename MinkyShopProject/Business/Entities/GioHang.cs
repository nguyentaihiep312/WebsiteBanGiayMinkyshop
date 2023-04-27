using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class GioHang
    {
        public Guid Id { get; set; }

        public Guid IdKhachHang { get; set; }

        public DateTime NgayTao { get; set; }

        public KhachHang KhachHang { get; set; } = null!;

        public List<GioHangChiTiet> GioHangChiTiets { get; set; } = null!;
    }
}
