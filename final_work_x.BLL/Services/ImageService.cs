using Microsoft.AspNetCore.Http;

namespace final_work_x.BLL.Services
{
    public class ImageService
    {
        public async Task<ServiceResponse> SaveAsync(IFormFile file, string dirPath)
        {
            try
            {
                var types = file.ContentType.Split("/");

                if (types.Length != 2 || types[0] != "image")
                {
                    return ServiceResponse.Error($"Файл '{file.FileName}' не є зображенням");
                }

                string imageName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string imagePath = Path.Combine(dirPath, imageName);

                using var fileStream = File.OpenWrite(imagePath);
                await file.CopyToAsync(fileStream);

                return ServiceResponse.Success($"Зображення '{file.FileName}' успішно збережено", imageName);

            }
            catch (Exception ex)
            {
                return ServiceResponse.Error(ex.Message);
            }
        }

        public ServiceResponse Delete(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return new ServiceResponse
                {
                    Message = "Зображення успішно видалено"
                };
            }

            return ServiceResponse.Error($"Зображення '{imagePath}' не знайдено");
        }
    }
}
