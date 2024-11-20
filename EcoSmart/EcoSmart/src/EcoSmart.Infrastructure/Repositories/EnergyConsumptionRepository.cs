using EcoSmart.Domain.Entities;
using EcoSmart.Infrastructure.Interfaces;
using EcoSmart.Infrastructure.Data;

namespace EcoSmart.Infrastructure.Repositories
{
    public class EnergyConsumptionRepository : IEnergyConsumptionRepository
    {
        private readonly AppDbContext _context;

        public EnergyConsumptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(EnergyConsumption consumo)
        {
            await _context.Consumptions.AddAsync(consumo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EnergyConsumption>> ObterPorDeviceIdAsync(
            string deviceId, DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            var query = _context.Consumptions.AsQueryable();

            if (!string.IsNullOrEmpty(deviceId))
            {
                query = query.Where(c => c.DeviceId == deviceId);
            }

            if (dataInicio.HasValue)
            {
                query = query.Where(c => c.Data >= dataInicio.Value);
            }

            if (dataFim.HasValue)
            {
                query = query.Where(c => c.Data <= dataFim.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<decimal> ObterConsumoTotalAsync()
        {
            return await _context.Consumptions.SumAsync(c => c.Amount);
        }

        public async Task<decimal> ObterPercentualEconomiaAsync()
        {
            return 10m;
        }

        public async Task<decimal> ObterMetaMensalAsync()
        {
    
            return 1000m;
        }
    }
}
