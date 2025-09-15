using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Domain.Entities;
using Million.RealEstate.Backend.Infrastructure.Data;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Infrastructure.Repositories;

public class PropertyImageRepository : IPropertyImageRepository
{
    private readonly IMongoCollection<PropertyImage> _collection;

    public PropertyImageRepository(PruebaMillionContext context)
    {
        _collection = context.PropertyImages;
    }

    public async Task<List<PropertyImage>> GetByPropertyIdAsync(string propertyId)
    {
        return await _collection.Find(img => img.IdProperty == propertyId).ToListAsync();
    }

    public async Task<PropertyImage?> GetByIdAsync(string id)
    {
        return await _collection.Find(img => img.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(PropertyImage image)
    {
        await _collection.InsertOneAsync(image);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(img => img.Id == id);
    }
}
