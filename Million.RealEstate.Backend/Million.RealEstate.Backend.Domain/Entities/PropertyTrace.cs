using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Million.RealEstate.Backend.Domain.Entities;

public class PropertyTrace
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string IdProperty { get; set; }

    public DateTime DateSale { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }
    public double Tax { get; set; }
}
