using EcoSmart.Domain.Entities;

namespace EcoSmart.Infrastructure.Interfaces
{
    public interface IDeviceRepository
    {
        Task<Device?> GetByIdAsync(string id);
        Task<IEnumerable<Device>> GetUserDevicesAsync(Guid userId);
        Task<Device> AddAsync(Device device);
        Task UpdateAsync(Device device);
        Task DeleteAsync(string id);
    }
}