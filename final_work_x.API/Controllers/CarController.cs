using final_work_x.API.Extensions;
using final_work_x.API.Settings;
using final_work_x.BLL.Dtos.Car;
using final_work_x.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace final_work_x.API.Controllers
{
    [ApiController]
    [Route("api/car")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;
        private readonly string _carsPath;

        public CarController(CarService carService, IWebHostEnvironment environment)
        {
            _carService = carService;

            string rootPath = environment.ContentRootPath;
            _carsPath = Path.Combine(rootPath, StaticFilesSettings.StorageDir, StaticFilesSettings.CarsDir);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _carService.GetAllAsync();
            return this.GetAction(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _carService.GetByIdAsync(id);
            return this.GetAction(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateCarDto dto)
        {
            var response = await _carService.CreateAsync(dto, _carsPath);
            return this.GetAction(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateCarDto dto)
        {
            var response = await _carService.UpdateAsync(dto, _carsPath);
            return this.GetAction(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _carService.DeleteAsync(id, _carsPath);
            return this.GetAction(response);
        }
    }
}