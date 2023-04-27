using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.BienThe
{
    public interface IBienTheRepository
    {
        public Task<Response> AddAsync(BienTheCreateModel obj);

        public Task<Response> DeleteAsync(Guid id);

        public Task<Response> UpdateAsync(Guid id, BienTheModel obj);

        public Task<Response> FindAsync(Guid id);

        public Task<Response> FindAsync(string ma);
    }
}
