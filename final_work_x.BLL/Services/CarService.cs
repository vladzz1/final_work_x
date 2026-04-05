using final_work_x.BLL.Dtos.Car;
using final_work_x.BLL.EntityConverters;
using final_work_x.DAL.Repositories;

namespace final_work_x.BLL.Services
{
    public class CarService
    {
        private readonly CarRepository _carRepository;
        private readonly ImageService _imageService;

        public CarService(CarRepository carRepository, ImageService imageService)
        {
            _carRepository = carRepository;
            _imageService = imageService;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var dtos = await CarConverter.EntityToDtoAsync(_carRepository);
            return ServiceResponse.Success("Автомобілі отримано", dtos);
        }

        public async Task<ServiceResponse> GetAllAsync(string property, string value)
        {
            var dtos = await CarConverter.EntityToDtoAsync(_carRepository, property, value);

            if (dtos == null || dtos.Count == 0)
            {
                return ServiceResponse.Error($"Автомобілів {property} {value} не існує");
            }

            return ServiceResponse.Success("Автомобілі отримано", dtos);
        }

        public async Task<ServiceResponse> GetAllAsync(double minValue, double maxValue)
        {
            var dtos = await CarConverter.EntityToDtoAsync(_carRepository, minValue, maxValue);

            if (dtos!.Count == 0)
            {
                return ServiceResponse.Error($"Автомобілів з ціневим діапазоном від {minValue} до {maxValue} не існує");
            }

            return ServiceResponse.Success("Автомобілі отримано", dtos);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _carRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Автомобіля з id {id} не існує");
            }

            return ServiceResponse.Success("Автомобіль успішно отриманий", CarConverter.EntityToDto(entity));
        }

        public async Task<ServiceResponse> CreateAsync(CreateCarDto dto, string imagesPath)
        {
            var entity = CarConverter.CreateDtoToEntity(dto);

            if (dto.Image != null && !string.IsNullOrEmpty(imagesPath))
            {
                ServiceResponse response = await _imageService.SaveAsync(dto.Image, imagesPath);

                if (!response.IsSuccess)
                {
                    return response;
                }

                entity.Image = response.Payload!.ToString()!;
            }

            bool res = await _carRepository.CreateAsync(entity);

            if (!res)
            {
                return ServiceResponse.Error("Не вдалося додати книгу");
            }

            return ServiceResponse.Success($"Автомобіль '{entity.Name}' успішно доданий", CarConverter.EntityToDto(entity));
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCarDto dto, string imagesPath)
        {
            var entity = await _carRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Автомобіля з id {dto.Id} не існує");
            }

            string oldName = entity.Name;
            CarConverter.UpdateDtoToEntity(dto, ref entity);

            if (dto.Image != null && !string.IsNullOrEmpty(imagesPath))
            {
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    string imagePath = Path.Combine(imagesPath, entity.Image);
                    var deleteResponse = _imageService.Delete(imagePath);

                    if (!deleteResponse.IsSuccess)
                    {
                        return deleteResponse;
                    }
                }

                var saveResponse = await _imageService.SaveAsync(dto.Image, imagesPath);

                if (!saveResponse.IsSuccess)
                {
                    return saveResponse;
                }

                entity.Image = saveResponse.Payload!.ToString()!;
            }

            bool res = await _carRepository.UpdateAsync(entity);

            if (!res)
            {
                return ServiceResponse.Error("Не вдалося оновити автомобіль");
            }

            return ServiceResponse.Success($"Автомобіль '{oldName}' успішно оновлений",CarConverter.EntityToDto(entity));
        }

        public async Task<ServiceResponse> DeleteAsync(int id, string imagesPath)
        {
            var entity = await _carRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Автомобіля з id {id} не існує");
            }

            if (!string.IsNullOrEmpty(entity.Image))
            {
                string imagePath = Path.Combine(imagesPath, entity.Image);
                var response = _imageService.Delete(imagePath);

                if (!response.IsSuccess)
                {
                    return response;
                }
            }

            bool res = await _carRepository.DeleteAsync(entity);

            if (!res)
            {
                return ServiceResponse.Error("Не вдалося видалити автомобіль");
            }

            return ServiceResponse.Success($"Автомобіль '{entity.Name}' успішно видалений", CarConverter.EntityToDto(entity));
        }
    }
}
