using AutoMapper;
using Million.RealEstate.Backend.Application.Common.Exceptions;
using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Application.Interfaces;

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

    public async Task<List<PropertyTraceDto>> GetByPropertyIdAsync(string propertyId)
    {
        if (string.IsNullOrEmpty(propertyId))
        {
            var errors = new Dictionary<string, string[]>
            {
                ["Id"] = ["El ID no puede estar vacío"]
            };
            throw new ValidationException(errors);
        }

        var propertyTraces = await _propertyTraceRepository.GetByPropertyIdAsync(propertyId);

        return _mapper.Map<List<PropertyTraceDto>>(propertyTraces);
    }
}
