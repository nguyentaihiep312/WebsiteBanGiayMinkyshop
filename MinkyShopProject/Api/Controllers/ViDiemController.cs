using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Repositories.ViDiem;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViDiemController : ControllerBase
    {
        private readonly IViDiemRepository _IViDiemRepository;

        public ViDiemController(IViDiemRepository IViDiemRepository)
        {
            _IViDiemRepository = IViDiemRepository;
        }

        [HttpGet, Route("Get")]
        public async Task<IActionResult> GetViDiem([FromQuery] ViDiemQueryModel item)
        {
            var khachHangRepo = await _IViDiemRepository.GetViDiem(item);
            return Helper.TransformData(khachHangRepo);
        }

        [HttpGet, Route("{Id}/GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var result = await _IViDiemRepository.GetById(Id);
            return Helper.TransformData(result);
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> CreateViDiem([FromQuery] ViDiemThemVaSuaModel model)
        {
            var result = await _IViDiemRepository.CreateViDiem(model);
            return Helper.TransformData(result);
        }

        [HttpPut, Route("Update")]
        public async Task<IActionResult> UpdateViDiem(Guid Id, [FromQuery] ViDiemThemVaSuaModel model)
        {
            var result = await _IViDiemRepository.UpdateViDiem(Id, model);
            return Helper.TransformData(result);
        }

        [HttpDelete, Route("{Id}/Delete")]
        public async Task<IActionResult> DeleteViDiem(Guid Id)
        {
            var result = await _IViDiemRepository.DeleteViDiem(Id);
            return Helper.TransformData(result);
        }
    }
}
