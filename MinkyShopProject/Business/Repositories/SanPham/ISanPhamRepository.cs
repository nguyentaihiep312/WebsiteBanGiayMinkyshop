using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Business.Repositories.SanPham
{
    public interface ISanPhamRepository
    {
        public Task<Response> GetAsync(SanPhamQueryModel obj);

        public Task<Response> UpdateAsync(Guid id, SanPhamModel obj);

        public Task<Response> DeleteAsync(Guid id);

        public Task<Response> FindAsync(Guid id);
    }
}
