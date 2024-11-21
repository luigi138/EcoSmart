using System;
using EcoSmart.Domain.Enums;

namespace EcoSmart.Domain.Entities
{
    public class EnergyConsumption
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; }

        private EnergyConsumption() 
        {
            Id = Guid.NewGuid();
            DeviceId = Guid.Empty;
            Timestamp = DateTime.UtcNow;
            Type = string.Empty;
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
                DeviceId = Guid.Parse(deviceId),  // 转换为 Guid 类型
                Amount = (decimal)amount,  // 转换为 decimal 类型
                Type = type.ToString(),
                Timestamp = DateTime.UtcNow
            };
        }
    }
}
