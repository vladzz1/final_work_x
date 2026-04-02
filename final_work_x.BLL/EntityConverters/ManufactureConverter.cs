using final_work_x.BLL.Dtos.Manufacture;
using final_work_x.DAL.Entities;
using final_work_x.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace final_work_x.BLL.EntityConverters
{
    internal class ManufactureConverter
    {
        public static ManufactureDto EntityToDto(ManufactureEntity entity)
        {
            return new ManufactureDto()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async static Task<List<ManufactureDto>?> EntityToDtoAsync(ManufactureRepository manufactureRepository)
        {
            return await manufactureRepository.Manufactures.Select(c => new ManufactureDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();
        }

        public static ManufactureEntity CreateDtoToEntity(CreateManufactureDto dto)
        {
            return new ManufactureEntity()
            {
                Name = dto.Name
            };
        }

        public static void UpdateDtoToEntity(UpdateManufactureDto dto, ref ManufactureEntity entity)
        {
            entity.Name = dto.Name;
        }
    }
}
