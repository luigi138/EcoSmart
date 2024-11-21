using EcoSmart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoSmart.Infrastructure.Interfaces
{
    public interface IEnergyConsumptionRepository
    {
        Task AdicionarAsync(EnergyConsumption consumo);
        Task<IEnumerable<EnergyConsumption>> GetByDeviceIdAsync(string deviceId, DateTime? dataInicio = null, DateTime? dataFim = null);
        Task<decimal> GetTotalConsumptionAsync();
        Task<decimal> GetSavingsPercentageAsync();
        Task<decimal> GetMonthlyGoalAsync();

        Task AddAsync(EnergyConsumption consumo); 
    }
}
