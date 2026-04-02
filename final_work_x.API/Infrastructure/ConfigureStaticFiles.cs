using final_work_x.API.Settings;
using Microsoft.Extensions.FileProviders;

namespace final_work_x.API.Infrastructure
{
    public static class ConfigureStaticFiles
    {
        public static IApplicationBuilder UseStaticFiles(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var items = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(StaticFilesSettings.ShareDir, StaticFilesSettings.ShareUrl),
                new KeyValuePair<string, string>(StaticFilesSettings.CarsDir, StaticFilesSettings.CarUrl)
            };

            string storagePath = Path.Combine(env.ContentRootPath, StaticFilesSettings.StorageDir);
            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }

            foreach (var item in items)
            {
                string path = Path.Combine(storagePath, item.Key);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(path),
                    RequestPath = item.Value
                });
            }

            return app;
        }
    }
}
