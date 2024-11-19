using EcoSmart.Domain.Enums;

namespace EcoSmart.Domain.Entities
{
    public class Device
    {
        public string Id { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public DeviceType Type { get; private set; }
        public string Location { get; private set; } = string.Empty;
        public DeviceStatus Status { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public Guid UserId { get; private set; }

        private Device() 
        {
            Id = string.Empty;
            Name = string.Empty;
            Location = string.Empty;
            LastUpdated = DateTime.UtcNow;
            Status = DeviceStatus.Active;
        }

        public static Device Create(string name, DeviceType type, string location, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentException("Location cannot be empty", nameof(location));

            return new Device
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Type = type,
                Location = location,
                Status = DeviceStatus.Active,
                LastUpdated = DateTime.UtcNow,
                UserId = userId
            };
        }

        public void UpdateStatus(DeviceStatus newStatus)
        {
            Status = newStatus;
            LastUpdated = DateTime.UtcNow;
        }
    }
}