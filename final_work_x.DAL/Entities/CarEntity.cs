namespace final_work_x.DAL.Entities
{
    public class CarEntity : BaseEntity
    {
        public required string Name { get; set; }
        public int Year { get; set; } = 2000;
        public int Volume { get; set; } = 1;
        public int Price { get; set; } = 1;
        public required string Color { get; set; }
        public string? Desciption { get; set; }
        public string? Image { get; set; }

        public int? ManufactureId { get; set; }
        public ManufactureEntity? Manufacture { get; set; }
    }
}
