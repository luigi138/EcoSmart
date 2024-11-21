
using Xunit;
using Moq;
using EcoSmart.Core.Services;
using EcoSmart.Abstractions.Interfaces;
using EcoSmart.Domain.Entities;

namespace EcoSmart.Tests
{
    public class DeviceServiceTests
    {
        [Fact]
        public void AddDevice_Should_Add_New_Device()
        {
            // Arrange
            var mockDeviceRepository = new Mock<IDeviceRepository>();
            var service = new DeviceService(mockDeviceRepository.Object, null);
            var device = Device.Create("Test Device", Domain.Enums.DeviceType.SmartMeter, "Test Location", Guid.NewGuid());

            // Act
            service.AddDevice(device);

            // Assert
            mockDeviceRepository.Verify(repo => repo.AddAsync(It.IsAny<Device>()), Times.Once);
        }
    }
}
