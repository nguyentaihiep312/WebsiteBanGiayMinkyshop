using MinkyShopProject.Business.Pagination;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaginationRequest = MinkyShopProject.Business.Pagination.PaginationRequest;

namespace MinkyShopProject.Business.Repositories.NhanVien
{
    public interface INhanVienRepository
    {
        Task<Response> Get(PaginationRequest paginationRequest);
        Task<Response> GetById(Guid Id);
        Task<Response> Post(Entities.NhanVien NhanVien);
        Task<Response> Put(Entities.NhanVien NhanVien);
        Task<Response> Delete(Guid Id);
        Task<Response> ChangeStatus(Guid Id,int status);
        Task<Response> Login(LoginModels.Login model);
    }
}
