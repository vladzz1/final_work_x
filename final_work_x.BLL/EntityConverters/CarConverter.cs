using final_work_x.BLL.Dtos.Car;
using final_work_x.BLL.Dtos.Manufacture;
using final_work_x.DAL.Entities;
using final_work_x.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace final_work_x.BLL.EntityConverters
{
    internal static class CarConverter
    {
        public static CarDto EntityToDto(CarEntity entity)
        {
            return new CarDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Year = entity.Year,
                Volume = entity.Volume,
                Price = entity.Price,
                Color = entity.Color,
                Description = entity.Description,
                Image = entity.Image,
                Manufacture = entity.Manufacture != null ? new ManufactureDto
                {
                    Id = entity.Manufacture.Id,
                    Name = entity.Manufacture.Name
                } : null
            };
        }

        public async static Task<List<CarDto>?> EntityToDtoAsync(CarRepository carRepository)
        {
            return await carRepository.Cars.Include(c => c.Manufacture).Select(c => new CarDto
            {
                Id = c.Id,
                Name = c.Name,
                Year = c.Year,
                Volume = c.Volume,
                Price = c.Price,
                Color = c.Color,
                Description = c.Description,
                Image = c.Image,
                Manufacture = c.Manufacture != null ? new ManufactureDto
                {
                    Id = c.Manufacture.Id,
                    Name = c.Manufacture.Name
                } : null
            }).ToListAsync();
        }

        public static CarEntity CreateDtoToEntity(CreateCarDto dto)
        {
            return new CarEntity()
            {
                Name = dto.Name,
                Year = dto.Year,
                Volume = dto.Volume,
                Price = dto.Price,
                Color = dto.Color,
                Description = dto.Description,
                Manufacture = dto.Manufacture
            };
        }

        public static void UpdateDtoToEntity(UpdateCarDto dto, ref CarEntity entity)
        {
            entity.Name = dto.Name;
            entity.Year = dto.Year;
            entity.Volume = dto.Volume;
            entity.Price = dto.Price;
            entity.Color = dto.Color;
            entity.Description = dto.Description;
            entity.Manufacture = dto.Manufacture;
        }
    }
}
