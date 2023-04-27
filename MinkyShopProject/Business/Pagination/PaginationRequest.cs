using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Pagination
{
    public class PaginationRequest
    {
        public int PerPage { get; set; }

        public int CurrentPage { get; set; }

        public int? Status { get; set; } = null!;

        public int? Role { get; set; } = null!;

        public string? Keyword { get; set; } = null!;
    }
}
