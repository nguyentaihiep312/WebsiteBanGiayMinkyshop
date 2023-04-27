using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Business.Repositories.ThuocTinh
{
    public interface IThuocTinhRepository
    {
        public Task<Response> GetAsync(ThuocTinhQueryModel obj);

        public Task<Response> AddAsync(ThuocTinhModel obj);

        public Task<Response> UpdateAsync(Guid id, ThuocTinhModel obj);

        public Task<Response> DeleteAsync(Guid id);
    }
}
