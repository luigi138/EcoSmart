using AutoMapper;
using EcoSmart.Core.DTOs;
using EcoSmart.Core.Interfaces;
using EcoSmart.Domain.Entities;
using EcoSmart.Infrastructure.Interfaces;

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

        public async Task<EnergyConsumptionDto> RecordConsumptionAsync(
            RecordConsumptionRequest request)
        {
            var device = await _deviceRepository.GetByIdAsync(request.DeviceId);
            if (device == null)
                throw new NotFoundException($"Device not found: {request.DeviceId}");

            var consumption = EnergyConsumption.Create(
                request.DeviceId,
                request.Amount,
                request.Type);

            await _consumptionRepository.AddAsync(consumption);
            return _mapper.Map<EnergyConsumptionDto>(consumption);
        }

        public async Task<IEnumerable<EnergyConsumptionDto>> GetDeviceConsumptionHistoryAsync(
            string deviceId,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var consumptions = await _consumptionRepository
                .GetByDeviceIdAsync(deviceId, startDate, endDate);
                
            return _mapper.Map<IEnumerable<EnergyConsumptionDto>>(consumptions);
        }
    }
}