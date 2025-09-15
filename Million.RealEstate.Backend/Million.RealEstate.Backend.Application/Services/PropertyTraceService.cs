using AutoMapper;
using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Services;

public class PropertyTraceService : IPropertyTraceService
{
    private readonly IPropertyTraceRepository _propertyTraceRepository;
    private readonly IMapper _mapper;

    public PropertyTraceService(IPropertyTraceRepository propertyTraceRepository, IMapper mapper)
    {
        _propertyTraceRepository = propertyTraceRepository;
        _mapper = mapper;
    }
    public Task CreateAsync(PropertyTrace trace)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PropertyTraceDto>> GetByPropertyIdAsync(string propertyId)
    {
        var propertyTraces = await _propertyTraceRepository.GetByPropertyIdAsync(propertyId);

        return _mapper.Map<List<PropertyTraceDto>>(propertyTraces);
    }
}
