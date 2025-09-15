using Microsoft.Extensions.Logging;
using Million.RealEstate.Backend.Application.Common.Exceptions;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Core.DTOs;
using Million.RealEstate.Backend.Domain.Entities;
using Million.RealEstate.Backend.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly IMongoCollection<Property> _property;
    private readonly ILogger<PropertyRepository> _logger;

    public PropertyRepository(PruebaMillionContext context, ILogger<PropertyRepository> logger)
    {
        _property = context.Properties;
        _logger = logger;
    }

    public async Task<(List<Property> Properties, long Total)> GetFilteredAsync(PropertyFilterDto filterDto)
    {
        var filters = Builders<Property>.Filter.Empty;

        if (!string.IsNullOrEmpty(filterDto.IdOwner))
        {
            filters &= Builders<Property>.Filter.Eq(p => p.IdOwner, filterDto.IdOwner);
        }

        if (!string.IsNullOrEmpty(filterDto.Name))
        {
            filters &= Builders<Property>.Filter.Regex(p => p.Name, new BsonRegularExpression(filterDto.Name, "i"));
        }

        if (!string.IsNullOrEmpty(filterDto.Address))
        {
            filters &= Builders<Property>.Filter.Regex(p => p.Address, new BsonRegularExpression(filterDto.Address, "i"));
        }

        if (filterDto.MinPrice.HasValue)
        {
            filters &= Builders<Property>.Filter.Gte(p => p.Price, filterDto.MinPrice.Value);
        }

        if (filterDto.MaxPrice.HasValue)
        {
            filters &= Builders<Property>.Filter.Lte(p => p.Price, filterDto.MaxPrice.Value);
        }

        var sortBuilder = Builders<Property>.Sort;
        var sort = filterDto.SortDirection.ToLower() == "desc"
            ? sortBuilder.Descending(filterDto.SortBy)
            : sortBuilder.Ascending(filterDto.SortBy);

        var total = await _property.CountDocumentsAsync(filters);

        var properties = await _property.Find(filters)
            .Sort(sort)
            .Skip((filterDto.Page - 1) * filterDto.Limit)
            .Limit(filterDto.Limit)
            .ToListAsync();

        return (properties, total);
    }

    public async Task<Property> GetByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            var errors = new Dictionary<string, string[]>
            {
                ["Id"] = ["El ID de la propiedad no es válido"]
            };
            throw new ValidationException(errors);
        }

        var filter = Builders<Property>.Filter.Eq(p => p.Id, id);
        return await _property.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Property property)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        await _property.InsertOneAsync(property);
        _logger.LogInformation("Propiedad creada con ID: {Id}", property.Id);
    }

    public async Task UpdateAsync(string id, Property property)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            var errors = new Dictionary<string, string[]>
            {
                ["Id"] = ["El ID de la propiedad no es válido"]
            };
            throw new ValidationException(errors);
        }


        var result = await _property.ReplaceOneAsync(p => p.Id == id, property);

        if (result.MatchedCount == 0)
            throw new NotFoundException(nameof(Property), id);

        _logger.LogInformation("Propiedad actualizada con ID: {Id}", id);
    }

    public async Task DeleteAsync(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            var errors = new Dictionary<string, string[]>
            {
                ["Id"] = ["El ID de la propiedad no es válido"]
            };
            throw new ValidationException(errors);
        }

        var result = await _property.DeleteOneAsync(p => p.Id == id);

        if (result.DeletedCount == 0)
            throw new NotFoundException(nameof(Property), id);

        _logger.LogInformation("Propiedad eliminada con ID: {Id}", id);
    }
}