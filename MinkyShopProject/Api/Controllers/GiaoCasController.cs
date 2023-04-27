using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Repositories.GiaoCa;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiaoCasController : ControllerBase
    {
        private readonly IGiaoCaRepositories _Repositories;

        public GiaoCasController(IGiaoCaRepositories Repositories)
        {
            _Repositories = Repositories;
        }

        [HttpPost]
        public async Task<IActionResult> KhaiBaoDauCa(GiaoCaModels.GiaoCaCreateModel model)
        {
            return Helper.TransformData(await _Repositories.KhaiBaoDauCa(model));
        }

        [HttpPost("KetCa/{Id}")]
        public async Task<IActionResult> KetCa(Guid Id,GiaoCaModels.GiaoCaEditModel model)
        {
            return Helper.TransformData(await _Repositories.KetCa(Id,model));
        }

        [HttpGet]
        public async Task<IActionResult> GetCaHienTai([FromQuery] Guid Id ,DateTime ThoiGian)
        {
            return Helper.TransformData(await _Repositories.GetCaHienTai(Id,ThoiGian));
        }

        [HttpGet("HoaDonTrongCa")]
        public async Task<IActionResult> GetHoaDonCaHienTai([FromQuery] Guid Id,DateTime ThoiGian)
        {
            return Helper.TransformData(await _Repositories.GetHoaDonCa(Id,ThoiGian));
        }

        [HttpGet("HoaDonCaDaKetThuc")]
        public async Task<IActionResult> HoaDonCaDaKetThuc([FromQuery] Guid Id, DateTime ThoiGian)
        {
            return Helper.TransformData(await _Repositories.GetHoaDonCaDaKetThuc(Id, ThoiGian));
        }

        [HttpGet("TienMatHoaDonTrongCa")]
        public async Task<IActionResult> TienMatHoaDon([FromQuery] Guid IdHoaDon)
        {
            return Helper.TransformData(await _Repositories.GetTienMatHoaDon(IdHoaDon));
        }

        [HttpGet("TienChuyenKhoanHoaDonTrongCa")]
        public async Task<IActionResult> TienChuyenKhoanHoaDon([FromQuery] Guid IdHoaDon)
        {
            return Helper.TransformData(await _Repositories.GetTienChuyenKhoanHoaDon(IdHoaDon));
        }

        [HttpGet("UpdateTienMat")]
        public async Task<IActionResult> UpdateTiemMat([FromQuery] Guid IdCa,float TongTien)
        {
            return Helper.TransformData(await _Repositories.UpdateTiemMat(IdCa, TongTien));
        }

        [HttpGet("GetAllCa")]
        public async Task<IActionResult> GetCaChoBanGiao([FromQuery] GiaoCaModels.GiaoCaQueryModel query)
        {
            return Helper.TransformData(await _Repositories.GetCa(query));
        }

        [HttpGet("CaDangCho")]
        public async Task<IActionResult> CaDangCho()
        {
            return Helper.TransformData(await _Repositories.CaDangCho());
        }

        [HttpGet("CaDuocChon")]
        public async Task<IActionResult> CaDuocChon([FromQuery] Guid Id)
        {
            return Helper.TransformData(await _Repositories.GetCaDuocChon(Id));
        }

        [HttpPut("resettienmat")]
        public async Task<IActionResult> resettienmat([FromQuery] Guid Id,GiaoCaModels.ResetTienModel model)
        {
            return Helper.TransformData(await _Repositories.RutTien(Id,model));
        }

        [HttpPut("UpdateNhanVien")]
        public async Task<IActionResult> UpdateNhanVien([FromQuery] Guid Id, GiaoCaModels.GiaoCaEditModel model)
        {
            return Helper.TransformData(await _Repositories.UpdateNhanVien(Id, model));
        }

        [HttpGet("GetCaTruoc")]
        public async Task<IActionResult> GetCaTruoc([FromQuery] Guid Id)
        {
            return Helper.TransformData(await _Repositories.GetCaTruoc(Id));
        }

        [HttpDelete("NhanCa")]
        public async Task<IActionResult> NhanCa([FromQuery] Guid Id)
        {
            return Helper.TransformData(await _Repositories.NhanCa(Id));
        }

        [HttpGet("GuiMail/{IdCa}")]
        public async Task<IActionResult> GuiMail(Guid IdCa)
        {
            return Helper.TransformData(await _Repositories.SendMail(IdCa));
        }
    }
}
