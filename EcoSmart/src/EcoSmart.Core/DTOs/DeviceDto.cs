using EcoSmart.Domain.Enums;

namespace EcoSmart.Core.DTOs
{
    public class DeviceDto
    {
        // 确保字段不为空，给它们设置默认值
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;  // 给 Name 字段设置默认值
        public DeviceType Type { get; set; }
        public string Location { get; set; } = string.Empty;  // 给 Location 字段设置默认值
        public DeviceStatus Status { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class CreateDeviceRequest
    {
        public string Name { get; set; } = string.Empty;  // 确保 Name 字段不为空
        public DeviceType Type { get; set; }
        public string Location { get; set; } = string.Empty;  // 确保 Location 字段不为空
        public Guid UserId { get; set; }
    }

    public class UpdateDeviceStatusRequest
    {
        public DeviceStatus Status { get; set; }
    }
}
