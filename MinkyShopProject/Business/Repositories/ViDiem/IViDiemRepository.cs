using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Business.Repositories.ViDiem
{
    public interface IViDiemRepository
    {
        public Task<Response> CreateViDiem(ViDiemThemVaSuaModel model);
        public Task<Response> UpdateViDiem(Guid Id, ViDiemThemVaSuaModel model);
        public Task<Response> DeleteViDiem(Guid Id);
        public Task<Response> GetViDiem(ViDiemQueryModel filter);
        public Task<Response> GetById(Guid Id);
    }
}
