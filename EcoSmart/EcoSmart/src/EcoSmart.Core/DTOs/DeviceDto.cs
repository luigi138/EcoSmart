using EcoSmart.Domain.Enums;

namespace EcoSmart.Core.DTOs
{
    public class DeviceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public string Location { get; set; }
        public DeviceStatus Status { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class CreateDeviceRequest
    {
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public string Location { get; set; }
        public Guid UserId { get; set; }
    }

    public class UpdateDeviceStatusRequest
    {
        public DeviceStatus Status { get; set; }
    }
}