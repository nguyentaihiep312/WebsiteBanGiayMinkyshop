using MinkyShopProject.Data.Enums;

namespace MinkyShopProject.Business.Entities
{
    public class ViDiem
    {
        public Guid Id { get; set; }

        public float TongDiem { get; set; }

        public float SoDiemDaDung { get; set; }

        public float SoDiemDaCong { get; set; }

        public int TrangThai { get; set; }

        public KhachHang KhachHang { get; set; } = null!;
    }
}
