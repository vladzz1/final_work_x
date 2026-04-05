using final_work_x.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace final_work_x.DAL.Initializer
{
    public static class Seeder
    {
        public static async Task SeedAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();

            // Cars, Manufactures

            var manufactures = new List<ManufactureEntity>();

            if (!context.Manufactures.Any())
            {
                manufactures.AddRange(
                    new ManufactureEntity { Name = "BYD" },
                    new ManufactureEntity { Name = "General Motors" },
                    new ManufactureEntity { Name = "Nissan" },
                    new ManufactureEntity { Name = "Chevrolet" },
                    new ManufactureEntity { Name = "Geely" }
                );

                await context.AddRangeAsync(manufactures);
                await context.SaveChangesAsync();
            }

            if (!context.Cars.Any())
            {
                if (manufactures.Count == 0)
                {
                    manufactures = await context.Manufactures.ToListAsync();
                }

                var cars = new List<CarEntity>()
                {
                    new CarEntity{ Name = "Volkswagen Golf", Year = 2023, Volume = 2.7f, Price = 58850, Color = "Gray", Description = "Luxury car", Image = null, Manufacture = new ManufactureEntity{ Name = "Volkswagen" } },
                    new CarEntity{ Name = "Ford F-Series", Year = 2024, Volume = 3.2f, Price = 62450, Color = null, Description = null, Image = null, Manufacture = new ManufactureEntity{ Name = "Ford" } },
                    new CarEntity{ Name = "Honda CR-V", Year = 2021, Volume = 2.2f, Price = 56200, Color = "Blue", Description = null, Image = null, Manufacture = new ManufactureEntity{ Name = "Honda" } },
                    new CarEntity{ Name = "Hyundai-Kia Sportage", Year = 2022, Volume = 2.6f, Price = 59550, Color = "black", Description = null, Image = null, Manufacture = new ManufactureEntity{ Name = "Hyundai-Kia" } },
                    new CarEntity{ Name = "Toyota RAV4", Year = 2023, Volume = 2.4f, Price = 57400, Color = null, Description = null, Image = null, Manufacture = new ManufactureEntity{ Name = "Toyota" } }
                };

                await context.Cars.AddRangeAsync(cars);
                await context.SaveChangesAsync();
            }
        }
    }
}