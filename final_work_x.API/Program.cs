using final_work_x.API.Infrastructure;
using final_work_x.API.Jobs;
using final_work_x.API.Middlewares;
using final_work_x.DAL;
using final_work_x.DAL.Initializer;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day).Enrich.FromLogContext().CreateLogger();

builder.Host.UseSerilog();

// Quartz
builder.Services.AddJobs(
    (typeof(LogsCleaningJob), "0 0 0 * * ?")
);

builder.Services.AddQuartzHostedService(cfg => cfg.WaitForJobsToComplete = true);

// Add repositories and services
builder.Services.AddRepositories().AddServices();

// Add dbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("LocalDb");
    options.UseNpgsql(connectionString);
});

// Add services to the container.

builder.Services.AddControllers();

// CORS - дозволяємо реакту кидати запити на наш бек
string corsName = "allowAll";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(corsName, cfg =>
    {
        cfg.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Custom middlewares
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Static files
app.UseStaticFiles(builder.Environment);

// CORS - дозволяємо реакту кидати запити на наш бек
app.UseCors(corsName);

app.MapControllers();

app.SeedAsync().Wait();

app.Run();