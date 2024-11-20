using EcoSmart.Domain.Entities;
using EcoSmart.Infrastructure.Data;
using EcoSmart.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcoSmart.Infrastructure.Repositories
{
   public class DeviceRepository : IDeviceRepository
   {
       private readonly EcoSmartDbContext _context;
       private readonly ILogger<DeviceRepository> _logger;

       public DeviceRepository(
           EcoSmartDbContext context,
           ILogger<DeviceRepository> logger)
       {
           _context = context;
           _logger = logger;
       }

       public async Task<Device?> GetByIdAsync(string id)
       {
           try
           {
               var device = await _context.Devices.FindAsync(id);
               return device;
           }
           catch (Exception ex)
           {
               _logger.LogError(ex, "Error retrieving device with ID: {Id}", id);
               throw;
           }
       }

       public async Task<IEnumerable<Device>> GetUserDevicesAsync(Guid userId)
       {
           try
           {
               return await _context.Devices
                   .Where(d => d.UserId == userId)
                   .ToListAsync();
           }
           catch (Exception ex)
           {
               _logger.LogError(ex, "Error retrieving devices for user: {UserId}", userId);
               throw;
           }
       }

       public async Task<Device> AddAsync(Device device)
       {
           try
           {
               await _context.Devices.AddAsync(device);
               await _context.SaveChangesAsync();
               return device;
           }
           catch (Exception ex)
           {
               _logger.LogError(ex, "Error adding device: {Id}", device.Id);
               throw;
           }
       }

       public async Task UpdateAsync(Device device)
       {
           try
           {
               _context.Devices.Update(device);
               await _context.SaveChangesAsync();
           }
           catch (Exception ex)
           {
               _logger.LogError(ex, "Error updating device: {Id}", device.Id);
               throw;
           }
       }

       public async Task DeleteAsync(string id)
       {
           try
           {
               var device = await GetByIdAsync(id);
               if (device != null)
               {
                   _context.Devices.Remove(device);
                   await _context.SaveChangesAsync();
               }
           }
           catch (Exception ex)
           {
               _logger.LogError(ex, "Error deleting device: {Id}", id);
               throw;
           }
       }
   }
}