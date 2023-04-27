using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Repositories.DanhGia;
using MinkyShopProject.Business.Repositories.NhomSanPham;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Business.Repositories.Voucher;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhGiaController : ControllerBase
    {
        private readonly IDanhGiaRepository _repository;

        public DanhGiaController(IDanhGiaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public async Task<ActionResult> GetAsync([FromQuery] DanhGiaQueryModel obj)
        {
            return Helper.TransformData(await _repository.GetAsync(obj));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindAsync(Guid id)
        {
            return Helper.TransformData(await _repository.FindAsync(id));
        }

        [HttpPost()]
        public async Task<ActionResult> AddAsync([FromBody] DanhGiaCreateModel obj)
        {
            return Helper.TransformData(await _repository.AddAsync(obj));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] DanhGiaCreateModel obj)
        {
            return Helper.TransformData(await _repository.UpdateAsync(id, obj));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            return Helper.TransformData(await _repository.DeleteAsync(id));
        }
    }
}
