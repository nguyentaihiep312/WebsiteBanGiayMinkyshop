using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Repositories.GioHang;
using MinkyShopProject.Business.Repositories.NhomSanPham;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Business.Repositories.ThuocTinh;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangController : ControllerBase
    {
        private readonly IGioHangRepository _repository;

        public GioHangController(IGioHangRepository repository)
        {
            _repository = repository;
        }

        [HttpPost()]
        public async Task<IActionResult> AddAsync([FromBody] GioHangModel obj)
        {
            return Helper.TransformData(await _repository.AddAsync(obj));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] GioHangModel obj)
        {
            return Helper.TransformData(await _repository.UpdateAsync(id, obj));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(Guid id, [FromQuery] GioHangQueryModel obj)
        {
            return Helper.TransformData(await _repository.GetAsync(id, obj));
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Helper.TransformData(await _repository.DeleteAsync(id));
        }
    }
}
