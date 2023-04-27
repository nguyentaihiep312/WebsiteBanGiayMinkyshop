using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;

namespace MinkyShopProject.Data.Models
{
    public class SanPhamModel
    {
        public Guid Id { get; set; }

        public Guid? IdNhomSanPham { get; set; }

        public string? Ten { get; set; }

        public string? Ma { get; set; }

        public string? Anh { get; set; }

        public string? MoTa { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public int TrangThai { get; set; }

        public NhomSanPhamModel? NhomSanPham { get; set; }

        public List<BienTheModel>? BienThes { get; set; }
    }

    public class SanPhamQueryModel : PaginationRequest
    {
        public string? Ten { get; set; } = "";

        public Guid IdNhomSanPham { get; set; }
    }
}
