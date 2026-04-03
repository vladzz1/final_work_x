using final_work_x.BLL.Services;
using final_work_x.DAL.Repositories;
using Quartz;

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

        public static IServiceCollection AddJobs(this IServiceCollection services, params (Type type, string cronSchedule)[] jobs)
        {
            services.AddQuartz(q =>
            {
                foreach (var job in jobs)
                {
                    var jobKey = new JobKey(job.type.Name);
                    q.AddJob(job.type, configure: opts => opts.WithIdentity(jobKey));

                    q.AddTrigger(opts => opts.ForJob(jobKey).WithIdentity($"{job.type.Name}-trigger").WithCronSchedule(job.cronSchedule));
                }
            });

            return services;
        }
    }
}
