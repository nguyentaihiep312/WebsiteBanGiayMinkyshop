using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Business.Repositories.DanhGia
{
    public interface IDanhGiaRepository
    {
        public Task<Response> GetAsync(DanhGiaQueryModel obj);

        public Task<Response> AddAsync(DanhGiaCreateModel obj);

        public Task<Response> UpdateAsync(Guid id, DanhGiaCreateModel obj);

        public Task<Response> DeleteAsync(Guid id);

        public Task<Response> FindAsync(Guid id);
    }
}
