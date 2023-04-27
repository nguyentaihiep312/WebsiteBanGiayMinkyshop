using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Business.Repositories.Voucher
{
    public interface IVoucherRepository
	{
        public Task<Response> GetAsync(VoucherQueryModel obj);

        public Task<Response> AddAsync(VoucherCreateModel obj);

        public Task<Response> UpdateAsync(Guid id, VoucherCreateModel obj);

        public Task<Response> DeleteAsync(Guid id);

        public Task<Response> FindAsync(string id);
    }
}
