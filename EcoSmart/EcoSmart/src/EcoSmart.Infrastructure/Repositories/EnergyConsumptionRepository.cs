using EcoSmart.Domain.Entities;
using EcoSmart.Infrastructure.Data;
using EcoSmart.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace EcoSmart.Infrastructure.Repositories
{
    public class EnergyConsumptionRepository : IEnergyConsumptionRepository
    {
        private readonly EcoSmartDbContext _context;

        public EnergyConsumptionRepository(EcoSmartDbContext context)
        {
            _context = context;
        }

        // 添加数据
        public async Task AdicionarAsync(EnergyConsumption consumo)
        {
            await _context.EnergyConsumptions.AddAsync(consumo);  // 使用 EnergyConsumptions
            await _context.SaveChangesAsync();  // 异步保存
        }

        // 根据设备 ID 获取数据
        public async Task<IEnumerable<EnergyConsumption>> GetByDeviceIdAsync(string deviceId, DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            var query = _context.EnergyConsumptions.AsQueryable();  // 使用 EnergyConsumptions

            if (!string.IsNullOrEmpty(deviceId))
            {
                query = query.Where(c => c.DeviceId == Guid.Parse(deviceId));  // 使用 Guid 类型
            }

            if (dataInicio.HasValue)
            {
                query = query.Where(c => c.Timestamp >= dataInicio.Value);
            }

            if (dataFim.HasValue)
            {
                query = query.Where(c => c.Timestamp <= dataFim.Value);
            }

            return await query.ToListAsync();  // 异步获取列表
        }

        // 获取总消耗量
        public async Task<decimal> GetTotalConsumptionAsync()
        {
            return await _context.EnergyConsumptions.SumAsync(c => c.Amount);  // 使用 EnergyConsumptions
        }

        // 获取节省的百分比
        public async Task<decimal> GetSavingsPercentageAsync()
        {
            return await Task.FromResult(10m);  // 返回 10% 的节省
        }

        // 获取每月目标
        public async Task<decimal> GetMonthlyGoalAsync()
        {
            return await Task.FromResult(1000m);  // 返回假设的目标 1000
        }

        // 添加异步方法：AddAsync
        public async Task AddAsync(EnergyConsumption consumo)
        {
            await _context.EnergyConsumptions.AddAsync(consumo);
            await _context.SaveChangesAsync();  // 异步保存
        }
    }
}
