using EcoSmart.Domain.Enums;

namespace EcoSmart.Domain.Entities
{
    public class EnergyConsumption
    {
        public Guid Id { get; private set; }
        public string DeviceId { get; private set; } = string.Empty;
        public double Amount { get; private set; }
        public DateTime Timestamp { get; private set; }
        public ConsumptionType Type { get; private set; }

        private EnergyConsumption() 
        {
            Id = Guid.NewGuid();
            DeviceId = string.Empty;
            Timestamp = DateTime.UtcNow;
        }

        public static EnergyConsumption Create(string deviceId, double amount, ConsumptionType type)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("DeviceId cannot be empty", nameof(deviceId));

            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));

            return new EnergyConsumption
            {
                Id = Guid.NewGuid(),
                DeviceId = deviceId,
                Amount = amount,
                Type = type,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}