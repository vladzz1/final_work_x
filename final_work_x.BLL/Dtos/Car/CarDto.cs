using final_work_x.DAL.Entities;

namespace final_work_x.BLL.Dtos.Car
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; }
        public float Volume { get; set; }
        public double Price { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public ManufactureEntity? Manufacture { get; set; }
    }
}
