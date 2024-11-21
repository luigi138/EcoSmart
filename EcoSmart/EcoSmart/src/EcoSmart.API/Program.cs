using EcoSmart.Core.Interfaces;
using EcoSmart.Core.Services;
using EcoSmart.Infrastructure.Data;
using EcoSmart.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using EcoSmart.Infrastructure.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 添加服务到容器
builder.Services.AddControllersWithViews(); // 启用 MVC 模式
builder.Services.AddEndpointsApiExplorer(); // 启用 API 终结点

// 配置 Swagger 用于 API 文档
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EcoSmart API",
        Version = "v1",
        Description = "API 用于能源消耗的监控和管理"
    });
});

// 配置 Oracle 数据库连接
builder.Services.AddDbContext<EcoSmartDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// 配置依赖注入
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IEnergyConsumptionRepository, EnergyConsumptionRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IEnergyConsumptionService, EnergyConsumptionService>();

// 配置 AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(DeviceService).Assembly); // 扫描映射
});

// 构建应用程序
var app = builder.Build();

// 测试数据库连接
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EcoSmartDbContext>();
    try
    {
        dbContext.Database.CanConnect();
        Console.WriteLine("Database connection successful.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}

// 配置中间件和环境依赖
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // 启用 Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcoSmart API v1");
    });
}

// 启用标准中间件
app.UseHttpsRedirection(); // 强制 HTTPS
app.UseStaticFiles();      // 提供静态文件支持
app.UseRouting();          // 启用路由
app.UseAuthorization();    // 启用授权

// 配置默认路由
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 启动应用
app.Run();
