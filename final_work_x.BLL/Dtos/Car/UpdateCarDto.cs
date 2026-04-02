using final_work_x.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace final_work_x.BLL.Dtos.Car
{
    public class UpdateCarDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; }
        public float Volume { get; set; }
        public double Price { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public ManufactureEntity? Manufacture { get; set; }
    }
}
