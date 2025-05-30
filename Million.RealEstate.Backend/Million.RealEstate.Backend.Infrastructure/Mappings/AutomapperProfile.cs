using AutoMapper;
using Million.RealEstate.Backend.Core.DTOs;
using Million.RealEstate.Backend.Core.Entities;

namespace Million.RealEstate.Backend.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Property, PropertyDto>().ReverseMap();
        }
    }
}
