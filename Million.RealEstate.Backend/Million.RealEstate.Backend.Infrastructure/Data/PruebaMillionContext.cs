using Microsoft.Extensions.Options;
using Million.RealEstate.Backend.Core.Entities;
using Million.RealEstate.Backend.Core.Helpers;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Infrastructure.Data
{
    public class PruebaMillionContext
    {
        private readonly IMongoDatabase _database;

        public PruebaMillionContext(IOptions<MongoDbSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            _database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        }

        public IMongoCollection<Property> Properties => _database.GetCollection<Property>("Properties");
    }
}
