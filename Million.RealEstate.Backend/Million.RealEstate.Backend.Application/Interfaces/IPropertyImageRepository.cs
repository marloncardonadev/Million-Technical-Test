using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Interfaces;

public interface IPropertyImageRepository
{
    Task<List<PropertyImage>> GetByPropertyIdAsync(string propertyId);
}
