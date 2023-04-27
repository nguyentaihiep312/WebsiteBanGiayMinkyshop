using System.ComponentModel.DataAnnotations;

namespace MinkyShopProject.Common
{
    public class PaginationRequest
    {
        public string Sort { get; set; } = "-LastModifiedOnDate";
        // public string Fields { get; set; }

        [Range(1, int.MaxValue)] public int? Page { get; set; } = 1;

        [Range(1, int.MaxValue)] public int? Size { get; set; } = 20;

        // public string Filter { get; set; } = "{}";
        // public string FullTextSearch { get; set; }
    }
}
