using Microsoft.Extensions.Logging;
using Million.RealEstate.Backend.Application.Common.Exceptions;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Domain.Entities;
using Million.RealEstate.Backend.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Infrastructure.Repositories;

public class PropertyTraceRepository : IPropertyTraceRepository
{
    private readonly IMongoCollection<PropertyTrace> _propertyTrace;
    private readonly ILogger<PropertyRepository> _logger;

    public PropertyTraceRepository(PruebaMillionContext propertyTrace, ILogger<PropertyRepository> logger)
    {
        _propertyTrace = propertyTrace.PropertyTraces;
        _logger = logger;
    }

    public async Task<List<PropertyTrace>> GetByPropertyIdAsync(string propertyId)
    {
        if (string.IsNullOrEmpty(propertyId) || !ObjectId.TryParse(propertyId, out _))
        {
            var errors = new Dictionary<string, string[]>
            {
                ["Id"] = ["El ID de la propiedad no es válido"]
            };
            throw new ValidationException(errors);
        }

        var filter = Builders<PropertyTrace>.Filter.Eq(p => p.IdProperty, propertyId);
        return await _propertyTrace.Find(filter).ToListAsync();
    }

    public async Task CreateAsync(PropertyTrace propertyTrace)
    {
        if (propertyTrace == null)
            throw new ArgumentNullException(nameof(propertyTrace));

        await _propertyTrace.InsertOneAsync(propertyTrace);
        _logger.LogInformation("Propiedad Trace creada con ID: {Id}", propertyTrace.Id);
    }
}
