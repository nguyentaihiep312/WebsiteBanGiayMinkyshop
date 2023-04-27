using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Repositories.HoaDon;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonRepository _repository;

        public HoaDonController(IHoaDonRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] HoaDonCreateModel obj)
        {
            return Helper.TransformData(await _repository.AddAsync(obj));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] HoaDonCreateModel obj)
        {
            return Helper.TransformData(await _repository.UpdateAsync(id, obj));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] HoaDonQueryModel obj)
        {
            return Helper.TransformData(await _repository.GetAsync(obj));
        }

        [HttpGet("GetHoaDonChuaHoanThanhAsync")]
        public async Task<IActionResult> GetHoaDonChuaHoanThanhAsync([FromQuery] HoaDonQueryModel obj)
        {
            return Helper.TransformData(await _repository.GetHoaDonChuaHoanThanhAsync(obj));
        }

        [HttpGet("khachhang/{id}")]
        public async Task<IActionResult> GetHoaDonKhachHangAsync(Guid id)
        {
            return Helper.TransformData(await _repository.GetHoaDonKhachHangAsync(id));
        }

		[HttpGet("ma/{id}")]
		public async Task<IActionResult> GetMa(string id)
		{
			return Helper.TransformData(await _repository.GetHoaDonByMaAsync(id));
		}

		[HttpGet("{id}")]
        public async Task<IActionResult> FindAsync(Guid id)
        {
            return Helper.TransformData(await _repository.FindAsync(id));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Helper.TransformData(await _repository.DeleteAsync(id));
        }
    }
}
