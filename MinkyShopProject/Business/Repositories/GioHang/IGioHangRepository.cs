using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.GioHang
{
    public interface IGioHangRepository
    {
        public Task<Response> GetAsync(Guid id, GioHangQueryModel obj);

        public Task<Response> AddAsync(GioHangModel obj);

        public Task<Response> UpdateAsync(Guid id, GioHangModel obj);

        public Task<Response> DeleteAsync(Guid id);
    }
}
