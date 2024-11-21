using EcoSmart.Core.Interfaces;
using EcoSmart.Core.Services;
using EcoSmart.Infrastructure.Data;
using EcoSmart.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using EcoSmart.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner
builder.Services.AddControllersWithViews(); // Habilita MVC
builder.Services.AddEndpointsApiExplorer(); // Habilita suporte a endpoints para APIs

// Configurações do Swagger para documentação da API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "EcoSmart API", 
        Version = "v1",
        Description = "API para monitoramento de consumo energético"
    });
});

// Configuração do banco de dados (Oracle)
builder.Services.AddDbContext<EcoSmartDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de repositórios no contêiner de injeção de dependências (DI)
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IEnergyConsumptionRepository, EnergyConsumptionRepository>();

// Registro de serviços no contêiner de injeção de dependências (DI)
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IEnergyConsumptionService, EnergyConsumptionService>();

// Configuração do AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(DeviceService).Assembly); // Escaneia mapeamentos no assembly
});

// Construção do aplicativo
var app = builder.Build();

// Configuração de middleware de acordo com o ambiente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita Swagger
    app.UseSwaggerUI(c => 
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcoSmart API v1"));
}

// Middleware padrão
app.UseHttpsRedirection(); // Força redirecionamento para HTTPS
app.UseStaticFiles();      // Habilita arquivos estáticos (CSS, JS, etc.)
app.UseRouting();          // Habilita roteamento
app.UseAuthorization();    // Middleware de autorização

// Configuração de rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Executa o aplicativo
app.Run();
