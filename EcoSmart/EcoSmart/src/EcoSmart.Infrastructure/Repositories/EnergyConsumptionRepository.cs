using EcoSmart.Domain.Entities;
using EcoSmart.Infrastructure.Data;
using EcoSmart.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcoSmart.Infrastructure.Repositories
{
    public class EnergyConsumptionRepository : IEnergyConsumptionRepository
    {
        private readonly EcoSmartDbContext _context;
        private readonly ILogger<EnergyConsumptionRepository> _logger;

        public EnergyConsumptionRepository(
            EcoSmartDbContext context,
            ILogger<EnergyConsumptionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<EnergyConsumption> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.EnergyConsumptions.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving consumption with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<EnergyConsumption>> GetByDeviceIdAsync(
            string deviceId,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            try
            {
                var query = _context.EnergyConsumptions
                    .Where(e => e.DeviceId == deviceId);

                if (startDate.HasValue)
                    query = query.Where(e => e.Timestamp >= startDate.Value);

                if (endDate.HasValue)
                    query = query.Where(e => e.Timestamp <= endDate.Value);

                return await query.OrderByDescending(e => e.Timestamp).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving consumptions for device: {DeviceId}", deviceId);
                throw;
            }
        }

        public async Task<EnergyConsumption> AddAsync(EnergyConsumption consumption)
        {
            try
            {
                await _context.EnergyConsumptions.AddAsync(consumption);
                await _context.SaveChangesAsync();
                return consumption;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding consumption record: {Id}", consumption.Id);
                throw;
            }
        }
    }
}