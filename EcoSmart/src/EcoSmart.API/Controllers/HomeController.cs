using Microsoft.AspNetCore.Mvc;
using EcoSmart.Core.Interfaces;

namespace EcoSmart.API.Controllers
{
   public class HomeController : Controller
   {
       private readonly IDeviceService _deviceService;
       private readonly IEnergyConsumptionService _energyService;

       public HomeController(IDeviceService deviceService, IEnergyConsumptionService energyService)
       {
           _deviceService = deviceService;
           _energyService = energyService;
       }

       public async Task<IActionResult> Index()
       {
           var today = DateTime.Today;
           var monthStart = new DateTime(today.Year, today.Month, 1);
           
           var activeDeviceCount = await _deviceService.GetActiveDeviceCountAsync();
           var devices = await _deviceService.GetUserDevicesAsync(Guid.Empty);
           var consumption = await _energyService.GetDeviceConsumptionHistoryAsync(
               string.Empty, 
               monthStart,
               today);

           ViewBag.DispositivosAtivos = activeDeviceCount;
           ViewBag.TotalDispositivos = await _deviceService.GetTotalDeviceCountAsync();
           ViewBag.Devices = devices;
           ViewBag.Consumption = consumption;
           ViewBag.DiasRestantes = DateTime.DaysInMonth(today.Year, today.Month) - today.Day;

           return View();
       }

       [HttpGet]
       public IActionResult Privacy()
       {
           return View();
       }

       [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
       public IActionResult Error()
       {
           return View();
       }
   }
}