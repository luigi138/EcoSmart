using EcoSmart.Domain.Enums;

namespace EcoSmart.Domain.Entities
{
    public class EnergyConsumption
    {
        public Guid Id { get; private set; }
        public string DeviceId { get; private set; }
        public double Amount { get; private set; }
        public DateTime Timestamp { get; private set; }
        public ConsumptionType Type { get; private set; }

        protected EnergyConsumption() { }

        public static EnergyConsumption Create(string deviceId, double amount, ConsumptionType type)
        {
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