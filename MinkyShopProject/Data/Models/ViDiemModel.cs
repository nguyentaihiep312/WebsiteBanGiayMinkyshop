using MinkyShopProject.Common;

namespace MinkyShopProject.Data.Models
{
    public class ViDiemModel
    {
        public Guid Id { get; set; }

        public float TongDiem { get; set; }

        public float SoDiemDaDung { get; set; }

        public float SoDiemDaCong { get; set; }
    }
    public class ViDiemThemVaSuaModel
    {

        public float TongDiem { get; set; }

        public float SoDiemDaDung { get; set; }

        public float SoDiemDaCong { get; set; }
    }

    public class ViDiemQueryModel : PaginationRequest
    {
        public Guid? vidiemId { get; set; }
    }
}
