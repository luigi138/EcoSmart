using EcoSmart.Domain.Enums;

namespace EcoSmart.Domain.Entities
{
    public class Device
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public DeviceType Type { get; private set; }
        public string Location { get; private set; }
        public DeviceStatus Status { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public Guid UserId { get; private set; }

        protected Device() { }

        public static Device Create(string name, DeviceType type, string location, Guid userId)
        {
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