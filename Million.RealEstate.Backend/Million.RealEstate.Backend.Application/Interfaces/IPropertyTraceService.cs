using Million.RealEstate.Backend.Application.DTOs;

namespace Million.RealEstate.Backend.Application.Interfaces;

public interface IPropertyTraceService
{
    Task<List<PropertyTraceDto>> GetByPropertyIdAsync(string propertyId);
}
