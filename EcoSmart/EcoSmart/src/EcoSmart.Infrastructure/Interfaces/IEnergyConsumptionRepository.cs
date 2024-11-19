using EcoSmart.Domain.Entities;

namespace EcoSmart.Infrastructure.Interfaces
{
    public interface IEnergyConsumptionRepository
    {
        Task<EnergyConsumption> GetByIdAsync(Guid id);
        Task<IEnumerable<EnergyConsumption>> GetByDeviceIdAsync(
            string deviceId, 
            DateTime? startDate = null,
            DateTime? endDate = null);
        Task<EnergyConsumption> AddAsync(EnergyConsumption consumption);
    }
}