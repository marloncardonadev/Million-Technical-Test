using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Interfaces;

public interface IPropertyImageService
{
    Task<List<PropertyImage>> GetByPropertyIdAsync(string propertyId);
    Task<PropertyImage?> GetByIdAsync(string id);
    Task CreateAsync(PropertyImage image);
    Task DeleteAsync(string id);
}
