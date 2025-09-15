using AutoMapper;
using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Property, PropertyDto>().ReverseMap();
        CreateMap<Owner, OwnerDto>().ReverseMap();
        CreateMap<PropertyImage, PropertyImageDto>().ReverseMap();
        CreateMap<PropertyTrace, PropertyTraceDto>().ReverseMap();
        CreateMap<Property, PropertySummaryDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
    }
}
