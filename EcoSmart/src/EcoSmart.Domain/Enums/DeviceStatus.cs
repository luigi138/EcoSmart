namespace EcoSmart.Domain.Enums
{
    /// <summary>
    /// 表示设备状态的枚举类型。
    /// </summary>
    public enum DeviceStatus
    {
        /// <summary>
        /// 设备处于激活状态，可以正常工作。
        /// </summary>
        Active = 1, // 激活状态

        /// <summary>
        /// 设备处于非激活状态，可能已关闭或不可用。
        /// </summary>
        Inactive = 2, // 非激活状态

        /// <summary>
        /// 设备正在维护中，不可使用。
        /// </summary>
        Maintenance = 3 // 维护状态
    }
}
