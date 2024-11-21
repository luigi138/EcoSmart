
using Xunit;
using Moq;
using EcoSmart.Core.Services;
using EcoSmart.Abstractions.Interfaces;
using EcoSmart.Domain.Entities;

namespace EcoSmart.Tests
{
    public class EnergyConsumptionServiceTests
    {
        [Fact]
        public async Task GetTotalConsumption_Should_Return_Correct_Value()
        {
            // Arrange
            var mockRepository = new Mock<IEnergyConsumptionRepository>();
            mockRepository.Setup(repo => repo.GetTotalConsumptionAsync()).ReturnsAsync(100.0m);
            var service = new EnergyConsumptionService(mockRepository.Object, null);

            // Act
            var result = await service.GetTotalConsumptionAsync();

            // Assert
            Assert.Equal(100.0m, result);
        }
    }
}
