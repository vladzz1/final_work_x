using System.ComponentModel.DataAnnotations;

namespace final_work_x.BLL.Dtos.Manufacture
{
    public class UpdateManufactureDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
