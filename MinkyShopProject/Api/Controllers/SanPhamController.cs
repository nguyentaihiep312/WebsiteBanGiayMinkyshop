using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [ApiExplorerSettings(GroupName = "SanPham")]
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamRepository _repository;

        public SanPhamController(ISanPhamRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindAsync(Guid id)
        {
            return Helper.TransformData(await _repository.FindAsync(id));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAsync([FromQuery] SanPhamQueryModel obj)
        {
            return Helper.TransformData(await _repository.GetAsync(obj));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SanPhamModel obj)
        {
            return Helper.TransformData(await _repository.UpdateAsync(id, obj));
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteSanPhamAsync(Guid id)
        {
            return Helper.TransformData(await _repository.DeleteAsync(id));
        }
    }
}
