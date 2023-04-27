using MinkyShopProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Pagination
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; } = new List<T>();

        public int Pages { get; set; }

        public int CurrentPage { get; set; }
    }
}
