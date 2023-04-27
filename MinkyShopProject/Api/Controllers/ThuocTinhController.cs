using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Repositories.ThuocTinh;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuocTinhController : ControllerBase
    {
        private readonly IThuocTinhRepository _repository;

        public ThuocTinhController(IThuocTinhRepository repository)
        {
            _repository = repository;
        }

        [HttpPost()]
        public async Task<IActionResult> AddAsync([FromBody] ThuocTinhModel obj)
        {
            return Helper.TransformData(await _repository.AddAsync(obj));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ThuocTinhModel obj)
        {
            return Helper.TransformData(await _repository.UpdateAsync(id, obj));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] ThuocTinhQueryModel obj)
        {
            return Helper.TransformData(await _repository.GetAsync(obj));
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Helper.TransformData(await _repository.DeleteAsync(id));
        }
    }
}
