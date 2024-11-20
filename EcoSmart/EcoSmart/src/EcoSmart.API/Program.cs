using EcoSmart.Core.Interfaces;
using EcoSmart.Core.Services;
using EcoSmart.Infrastructure.Data;
using EcoSmart.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using EcoSmart.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to container
builder.Services.AddControllersWithViews(); // MV
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

//Database
builder.Services.AddDbContext<EcoSmartDbContext>(options =>
   options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
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
app.UseStaticFiles(); // 添加静态文件
app.UseRouting();    // 添加路由
app.UseAuthorization();

// 添加
app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();