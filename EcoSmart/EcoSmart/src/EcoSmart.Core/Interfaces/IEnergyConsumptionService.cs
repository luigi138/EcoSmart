using EcoSmart.Core.DTOs;

namespace EcoSmart.Core.Interfaces
{
    public interface IEnergyConsumptionService
    {
        Task<EnergyConsumptionDto> RecordConsumptionAsync(RecordConsumptionRequest request);
        Task<IEnumerable<EnergyConsumptionDto>> GetDeviceConsumptionHistoryAsync(
            string deviceId,
            DateTime? startDate = null,
            DateTime? endDate = null);
    }
}