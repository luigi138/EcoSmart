using AutoMapper;
using EcoSmart.Core.DTOs;
using EcoSmart.Core.Interfaces;
using EcoSmart.Domain.Entities;
using EcoSmart.Domain.Enums;
using EcoSmart.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using EcoSmart.Core.Exceptions;




namespace EcoSmart.Core.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeviceService> _logger;

        public DeviceService(
            IDeviceRepository deviceRepository,
            IMapper mapper,
            ILogger<DeviceService> logger)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DeviceDto> GetByIdAsync(string id)
        {
            var device = await _deviceRepository.GetByIdAsync(id);
            if (device == null)
                throw new NotFoundException($"Device not found: {id}");

            return _mapper.Map<DeviceDto>(device);
        }

        public async Task<IEnumerable<DeviceDto>> GetUserDevicesAsync(Guid userId)
        {
            var devices = await _deviceRepository.GetUserDevicesAsync(userId);
            return _mapper.Map<IEnumerable<DeviceDto>>(devices);
        }

        public async Task<DeviceDto> CreateAsync(CreateDeviceRequest request)
        {
            ValidateCreateRequest(request);

            var device = Device.Create(
                request.Name,
                request.Type,
                request.Location,
                request.UserId);

            await _deviceRepository.AddAsync(device);
            return _mapper.Map<DeviceDto>(device);
        }

        public async Task UpdateStatusAsync(string id, DeviceStatus status)
        {
            var device = await _deviceRepository.GetByIdAsync(id);
            if (device == null)
                throw new NotFoundException($"Device not found: {id}");

            device.UpdateStatus(status);
            await _deviceRepository.UpdateAsync(device);
        }

        public async Task<int> GetActiveDeviceCountAsync()
        {
            return await _deviceRepository.GetActiveDeviceCountAsync();
        }

        public async Task<int> GetTotalDeviceCountAsync()
        {
            return await _deviceRepository.GetTotalDeviceCountAsync();
        }

        public async Task<IEnumerable<DeviceDto>> GetAllDevicesAsync()
        {
            var devices = await _deviceRepository.GetAllDevicesAsync();
            return _mapper.Map<IEnumerable<DeviceDto>>(devices);
        }

        private void ValidateCreateRequest(CreateDeviceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("Device name is required");

            if (string.IsNullOrWhiteSpace(request.Location))
                throw new ValidationException("Device location is required");
        }
    }
}
