using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Repositories.BienThe;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BienTheController : ControllerBase
    {
        private readonly IBienTheRepository _repository;

        public BienTheController(MinkyShopDbContext context, IBienTheRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(BienTheCreateModel obj)
        {
            return Helper.TransformData(await _repository.AddAsync(obj));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindAsync(Guid id)
        {
            return Helper.TransformData(await _repository.FindAsync(id));
        }

        [HttpGet("search/{ma}")]
        public async Task<IActionResult> FindAsync(string ma)
        {
            return Helper.TransformData(await _repository.FindAsync(ma));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, BienTheModel obj)
        {
            return Helper.TransformData(await _repository.UpdateAsync(id, obj));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Helper.TransformData(await _repository.DeleteAsync(id));
        }
    }
}
