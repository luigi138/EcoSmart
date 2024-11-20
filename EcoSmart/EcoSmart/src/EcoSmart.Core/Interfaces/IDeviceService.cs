using EcoSmart.Core.DTOs;
using EcoSmart.Domain.Enums;

namespace EcoSmart.Core.Interfaces
{
    public interface IDeviceService
    {
        Task<DeviceDto> GetByIdAsync(string id);
        Task<IEnumerable<DeviceDto>> GetUserDevicesAsync(Guid userId);
        Task<DeviceDto> CreateAsync(CreateDeviceRequest request);
        Task UpdateStatusAsync(string id, DeviceStatus status);

          Task<int> GetActiveDeviceCountAsync();
    Task<int> GetTotalDeviceCountAsync();
    Task<IEnumerable<DeviceDto>> GetAllDevicesAsync();
    }
}
