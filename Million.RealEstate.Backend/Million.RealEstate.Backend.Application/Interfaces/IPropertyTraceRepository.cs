using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Interfaces;

public interface IPropertyTraceRepository
{
    Task<List<PropertyTrace>> GetByPropertyIdAsync(string propertyId);
    Task CreateAsync(PropertyTrace trace);
}
