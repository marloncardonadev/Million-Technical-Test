using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Million.RealEstate.Backend.Domain.Entities;

public class Property
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string IdOwner { get; set; }

    public string Name { get; set; }
    public string Address { get; set; }
    public Decimal Price { get; set; }
    public string CodeInternal { get; set; }
    public int Year { get; set; }
}
