using final_work_x.BLL.Services;
using final_work_x.DAL.Repositories;

namespace final_work_x.API.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<CarService>();
            services.AddScoped<ImageService>();
            services.AddScoped<ManufactureService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<CarRepository>();
            services.AddScoped<ManufactureRepository>();

            return services;
        }
    }
}
