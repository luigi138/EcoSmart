using EcoSmart.Domain.Entities;
using EcoSmart.Domain.Enums;
using EcoSmart.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoSmart.Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DbContext _dbContext;

        public DeviceRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Device?> GetByIdAsync(string id)
        {
            return await _dbContext.Set<Device>().FindAsync(id);
        }

        public async Task<IEnumerable<Device>> GetUserDevicesAsync(Guid userId)
        {
            return await _dbContext.Set<Device>().Where(d => d.UserId == userId).ToListAsync();
        }

        public async Task AddAsync(Device device)
        {
            await _dbContext.Set<Device>().AddAsync(device);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Device device)
        {
            _dbContext.Set<Device>().Update(device);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetActiveDeviceCountAsync()
        {
            return await _dbContext.Set<Device>().CountAsync(d => d.Status == DeviceStatus.Active);
        }

        public async Task<int> GetTotalDeviceCountAsync()
        {
            return await _dbContext.Set<Device>().CountAsync();
        }

        public async Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            return await _dbContext.Set<Device>().ToListAsync();
        }
    }
}
