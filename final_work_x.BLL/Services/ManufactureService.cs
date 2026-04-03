using final_work_x.BLL.Dtos.Manufacture;
using final_work_x.BLL.EntityConverters;
using final_work_x.DAL.Repositories;

namespace final_work_x.BLL.Services
{
    public class ManufactureService
    {
        private readonly ManufactureRepository _manufactureRepository;

        public ManufactureService(ManufactureRepository manufactureRepository)
        {
            _manufactureRepository = manufactureRepository;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var dtos = await ManufactureConverter.EntityToDtoAsync(_manufactureRepository);
            return ServiceResponse.Success("Виробника отримано", dtos);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _manufactureRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Виробника з id {id} не існує");
            }

            return ServiceResponse.Success("Виробник успішно отриманий", ManufactureConverter.EntityToDto(entity));
        }

        public async Task<ServiceResponse> CreateAsync(CreateManufactureDto dto)
        {
            var entity = ManufactureConverter.CreateDtoToEntity(dto);

            bool res = await _manufactureRepository.CreateAsync(entity);

            if (!res)
            {
                return ServiceResponse.Error("Не вдалося додати виробника");
            }

            return ServiceResponse.Success($"Виробник '{entity.Name}' успішно доданий", ManufactureConverter.EntityToDto(entity));
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateManufactureDto dto)
        {
            var entity = await _manufactureRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Виробника з id {dto.Id} не існує");
            }

            string oldName = entity.Name;
            ManufactureConverter.UpdateDtoToEntity(dto, ref entity);

            bool res = await _manufactureRepository.UpdateAsync(entity);

            if (!res)
            {
                return ServiceResponse.Error("Не вдалося оновити виробника");
            }

            return ServiceResponse.Success($"Виробник '{oldName}' успішно оновлений", ManufactureConverter.EntityToDto(entity));
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var entity = await _manufactureRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Виробника з id {id} не існує");
            }

            bool res = await _manufactureRepository.DeleteAsync(entity);

            if (!res)
            {
                return ServiceResponse.Error("Не вдалося видалити виробника");
            }

            return ServiceResponse.Success($"Виробник '{entity.Name}' успішно видалений", ManufactureConverter.EntityToDto(entity));
        }
    }
}
