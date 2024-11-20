using EcoSmart.Domain.Entities;
using EcoSmart.Infrastructure.Interfaces; 


namespace EcoSmart.Infrastructure.Interfaces
{
    public interface IDeviceRepository
    {
        /// <summary>
        /// 根据设备ID获取设备信息
        /// </summary>
        /// <param name="id">设备ID</param>
        /// <returns>设备信息</returns>
        Task<Device?> GetByIdAsync(string id);  // 修改为 Task<Device?>，表示返回的设备可能为 null

        /// <summary>
        /// 根据用户ID获取用户关联的设备列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>设备列表</returns>
        Task<IEnumerable<Device>> GetUserDevicesAsync(Guid userId);

        /// <summary>
        /// 添加新设备
        /// </summary>
        /// <param name="device">设备对象</param>
        Task AddAsync(Device device);

        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <param name="device">设备对象</param>
        Task UpdateAsync(Device device);

        /// <summary>
        /// 获取激活状态的设备总数
        /// </summary>
        /// <returns>激活设备数量</returns>
        Task<int> GetActiveDeviceCountAsync();

        /// <summary>
        /// 获取设备总数
        /// </summary>
        /// <returns>设备总数</returns>
        Task<int> GetTotalDeviceCountAsync();

        /// <summary>
        /// 获取所有设备的列表
        /// </summary>
        /// <returns>设备列表</returns>
        Task<IEnumerable<Device>> GetAllDevicesAsync();
    }
}