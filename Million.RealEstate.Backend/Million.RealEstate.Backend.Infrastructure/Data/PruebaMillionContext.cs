using Microsoft.Extensions.Options;
using Million.RealEstate.Backend.Domain.Entities;
using Million.RealEstate.Backend.Infrastructure.Helpers;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Infrastructure.Data;

public class PruebaMillionContext
{
    private readonly IMongoDatabase _database;

    public PruebaMillionContext(IOptions<MongoDbSettings> mongoSettings)
    {
        var client = new MongoClient(mongoSettings.Value.ConnectionString);
        _database = client.GetDatabase(mongoSettings.Value.DatabaseName);
    }

    public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("Owner");
    public IMongoCollection<Property> Properties => _database.GetCollection<Property>("Property");
    public IMongoCollection<PropertyImage> PropertyImages => _database.GetCollection<PropertyImage>("PropertyImage");
    public IMongoCollection<PropertyTrace> PropertyTraces => _database.GetCollection<PropertyTrace>("PropertyTrace");
}
