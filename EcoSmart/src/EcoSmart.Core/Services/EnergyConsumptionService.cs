using AutoMapper;
using EcoSmart.Core.DTOs;
using EcoSmart.Core.Exceptions;
using EcoSmart.Core.Interfaces;
using EcoSmart.Domain.Entities;
using EcoSmart.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace EcoSmart.Core.Services
{
    public class EnergyConsumptionService : IEnergyConsumptionService
    {
        private readonly IEnergyConsumptionRepository _consumptionRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EnergyConsumptionService> _logger;

        public EnergyConsumptionService(
            IEnergyConsumptionRepository consumptionRepository,
            IDeviceRepository deviceRepository,
            IMapper mapper,
            ILogger<EnergyConsumptionService> logger)
        {
            _consumptionRepository = consumptionRepository;
            _deviceRepository = deviceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EnergyConsumptionDto> RecordConsumptionAsync(RecordConsumptionRequest request)
        {
            var device = await _deviceRepository.GetByIdAsync(request.DeviceId);
            if (device == null)
                throw new NotFoundException($"Device not found: {request.DeviceId}");

            var consumption = EnergyConsumption.Create(
                request.DeviceId,
                request.Amount,
                request.Type);

            await _consumptionRepository.AddAsync(consumption);
            _logger.LogInformation($"Recorded consumption for device {request.DeviceId}");

            return _mapper.Map<EnergyConsumptionDto>(consumption);
        }

        public async Task<IEnumerable<EnergyConsumptionDto>> GetDeviceConsumptionHistoryAsync(
            string deviceId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var consumptions = await _consumptionRepository
                .GetByDeviceIdAsync(deviceId, startDate, endDate);

            _logger.LogInformation($"Fetched consumption history for device {deviceId}");
            return _mapper.Map<IEnumerable<EnergyConsumptionDto>>(consumptions);
        }

        public async Task<decimal> GetTotalConsumptionAsync()
        {
            var totalConsumption = await _consumptionRepository.GetTotalConsumptionAsync();
            _logger.LogInformation($"Total energy consumption fetched: {totalConsumption} kWh");
            return totalConsumption;
        }

        public async Task<decimal> GetSavingsPercentageAsync()
        {
            var savingsPercentage = await _consumptionRepository.GetSavingsPercentageAsync();
            _logger.LogInformation($"Savings percentage fetched: {savingsPercentage}%");
            return savingsPercentage;
        }

        public async Task<decimal> GetMonthlyGoalAsync()
        {
            var monthlyGoal = await _consumptionRepository.GetMonthlyGoalAsync();
            _logger.LogInformation($"Monthly energy consumption goal fetched: {monthlyGoal} kWh");
            return monthlyGoal;
        }
    }
}
