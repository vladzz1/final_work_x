using final_work_x.API.Extensions;
using final_work_x.BLL.Dtos.Manufacture;
using final_work_x.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace final_work_x.API.Controllers
{
    [ApiController]
    [Route("api/manufacture")]
    public class ManufactureController : ControllerBase
    {
        private readonly ManufactureService _manufactureService;

        public ManufactureController(ManufactureService manufactureService)
        {
            _manufactureService = manufactureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _manufactureService.GetAllAsync();
            return this.GetAction(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _manufactureService.GetByIdAsync(id);
            return this.GetAction(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateManufactureDto dto)
        {
            var response = await _manufactureService.CreateAsync(dto);
            return this.GetAction(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateManufactureDto dto)
        {
            var response = await _manufactureService.UpdateAsync(dto);
            return this.GetAction(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _manufactureService.DeleteAsync(id);
            return this.GetAction(response);
        }
    }
}
