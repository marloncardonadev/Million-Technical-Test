using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Services;

public class PropertyImageService : IPropertyImageService
{
    public Task CreateAsync(PropertyImage image)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<PropertyImage?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PropertyImage>> GetByPropertyIdAsync(string propertyId)
    {
        throw new NotImplementedException();
    }
}
