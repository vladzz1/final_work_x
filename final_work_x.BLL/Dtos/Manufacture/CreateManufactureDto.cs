using System.ComponentModel.DataAnnotations;

namespace final_work_x.BLL.Dtos.Manufacture
{
    public class CreateManufactureDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
