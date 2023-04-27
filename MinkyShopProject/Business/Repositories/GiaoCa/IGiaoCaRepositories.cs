using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MinkyShopProject.Data.Models.GiaoCaModels;

namespace MinkyShopProject.Business.Repositories.GiaoCa
{
    public interface IGiaoCaRepositories
    {
        Task<Response> KhaiBaoDauCa(GiaoCaModels.GiaoCaCreateModel model);
        Task<Response> GetCaHienTai(Guid Id,DateTime ThoiGian);
        Task<Response> KetCa(Guid Id,GiaoCaModels.GiaoCaEditModel model);
        Task<Response> GetHoaDonCa(Guid IdNhanVien,DateTime ThoiGian);
        Task<Response> GetTienMatHoaDon(Guid IdHoaDon);
        Task<Response> GetTienChuyenKhoanHoaDon(Guid IdHoaDon);
        Task<Response> UpdateTiemMat(Guid IdCa,float TongTien);
        Task<Response> GetCa(GiaoCaModels.GiaoCaQueryModel query);
        Task<Response> GetCaDuocChon(Guid Id);
        Task<Response> RutTien(Guid Id, GiaoCaModels.ResetTienModel model);
        Task<Response> UpdateNhanVien(Guid Id,GiaoCaModels.GiaoCaEditModel model);
        Task<Response> GetCaTruoc(Guid IdNhanVien);
        Task<Response> NhanCa(Guid Id);
        Task<Response> CaDangCho();
        Task<Response> SendMail(Guid IdCa);

        Task<Response> GetHoaDonCaDaKetThuc(Guid IdNhanVien, DateTime ThoiGian);
    }
}
