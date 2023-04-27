using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Business.Repositories.KhachHang
{
	public interface IKhachHangRepository
	{
		Task<Response> CreateKhachHang(KhachHangThemVaSuaModel model);
		Task<Response> UpdateKhachHang(Guid Id, KhachHangThemVaSuaModel model);
		Task<Response> GetKhachHang(KhachHangQueryModel filter);
		Task<Response> GetbyIdKhachHang(Guid Id);
		Task<Response> DeleteKhachHang(Guid Id);
		Task<Response> Login(string username, string password);
		Task<Response> ForgotPassword(string email);
	}
}
