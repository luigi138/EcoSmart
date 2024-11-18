using EcoSmart.Domain.Enums;

namespace EcoSmart.Core.DTOs
{
    public class EnergyConsumptionDto
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public ConsumptionType Type { get; set; }
    }

    public class RecordConsumptionRequest
    {
        public string DeviceId { get; set; }
        public double Amount { get; set; }
        public ConsumptionType Type { get; set; }
    }
}