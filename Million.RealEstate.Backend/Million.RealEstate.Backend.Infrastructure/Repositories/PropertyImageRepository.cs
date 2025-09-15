using Million.RealEstate.Backend.Application.Common.Exceptions;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Domain.Entities;
using Million.RealEstate.Backend.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Infrastructure.Repositories;

public class PropertyImageRepository : IPropertyImageRepository
{
    private readonly IMongoCollection<PropertyImage> _propertyImage;

    public PropertyImageRepository(PruebaMillionContext propertyImage)
    {
        _propertyImage = propertyImage.PropertyImages;
    }

    public async Task<List<PropertyImage>> GetByPropertyIdAsync(string propertyId)
    {
        if (string.IsNullOrEmpty(propertyId) || !ObjectId.TryParse(propertyId, out _))
        {
            var errors = new Dictionary<string, string[]>
            {
                ["Id"] = ["El ID de la propiedad no es válido"]
            };
            throw new ValidationException(errors);
        }
        return await _propertyImage.Find(img => img.IdProperty == propertyId).ToListAsync();
    }
}
