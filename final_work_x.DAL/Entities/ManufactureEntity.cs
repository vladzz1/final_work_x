namespace final_work_x.DAL.Entities
{
    public class ManufactureEntity : BaseEntity
    {
        public required string Name { get; set; }

        public List<CarEntity> Cars { get; set; } = [];
    }
}
