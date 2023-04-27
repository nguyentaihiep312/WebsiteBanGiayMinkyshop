using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.NhomSanPham
{
    public interface INhomSanPhamRepository
    {
        public Task<Response> GetAsync(NhomSanPhamQueryModel obj);

        public Task<Response> GetAsync2(NhomSanPhamQueryModel obj);

        public Task<Response> AddAsync(NhomSanPhamModel obj);

        public Task<Response> UpdateAsync(Guid id, NhomSanPhamModel obj);

        public Task<Response> DeleteAsync(Guid id);
    }
}
