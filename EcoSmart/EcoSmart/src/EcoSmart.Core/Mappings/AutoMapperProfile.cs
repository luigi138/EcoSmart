using AutoMapper;
using EcoSmart.Core.DTOs;
using EcoSmart.Domain.Entities;

namespace EcoSmart.Core.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Device, DeviceDto>();
            CreateMap<EnergyConsumption, EnergyConsumptionDto>();
        }
    }
}