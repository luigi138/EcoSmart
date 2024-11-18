using EcoSmart.Core.Interfaces;
using EcoSmart.Core.Services;
using EcoSmart.Infrastructure.Data;
using EcoSmart.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services a container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "EcoSmart API", 
        Version = "v1",
        Description = "API para monitoramento de consumo energético"
    });
});

// 配置数据库
builder.Services.AddDbContext<EcoSmartDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IEnergyConsumptionRepository, EnergyConsumptionRepository>();

// Services
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IEnergyConsumptionService, EnergyConsumptionService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly, 
    typeof(DeviceService).Assembly);

var app = builder.Build();

// Configure HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcoSmart API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();