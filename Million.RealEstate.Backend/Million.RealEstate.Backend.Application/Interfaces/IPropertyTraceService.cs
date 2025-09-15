using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Interfaces;

public interface IPropertyTraceService
{
    Task<List<PropertyTraceDto>> GetByPropertyIdAsync(string propertyId);
    Task CreateAsync(PropertyTrace trace);
}
