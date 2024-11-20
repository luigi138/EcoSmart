using EcoSmart.Domain.Entities;

namespace EcoSmart.Infrastructure.Interfaces
{
    public interface IEnergyConsumptionRepository
    {
        Task AdicionarAsync(EnergyConsumption consumo);
        Task<IEnumerable<EnergyConsumption>> ObterPorDeviceIdAsync(
            string deviceId, DateTime? dataInicio = null, DateTime? dataFim = null);

        Task<decimal> ObterConsumoTotalAsync();
        Task<decimal> ObterPercentualEconomiaAsync();
        Task<decimal> ObterMetaMensalAsync();
    }
}
