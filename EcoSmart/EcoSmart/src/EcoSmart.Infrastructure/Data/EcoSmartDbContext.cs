using Microsoft.EntityFrameworkCore;
using EcoSmart.Domain.Entities;

namespace EcoSmart.Infrastructure.Data
{
    public class EcoSmartDbContext : DbContext
    {
        public EcoSmartDbContext(DbContextOptions<EcoSmartDbContext> options)
            : base(options)
        {
        }

        // 确保有 DbSet<EnergyConsumption> 定义，名称为 EnergyConsumptions
        public DbSet<EnergyConsumption> EnergyConsumptions => Set<EnergyConsumption>();

        public DbSet<Device> Devices => Set<Device>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("DEVICES");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.Location).HasMaxLength(200);
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.LastUpdated).IsRequired();
                entity.Property(e => e.UserId).IsRequired();

                entity.HasIndex(e => e.UserId);
            });

            modelBuilder.Entity<EnergyConsumption>(entity =>
            {
                entity.ToTable("ENERGY_CONSUMPTIONS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DeviceId).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.Timestamp).IsRequired();
                entity.Property(e => e.Type).IsRequired();
                
                entity.HasIndex(e => e.DeviceId);
                entity.HasIndex(e => e.Timestamp);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
