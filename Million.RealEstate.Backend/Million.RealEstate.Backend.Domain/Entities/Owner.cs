using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Million.RealEstate.Backend.Domain.Entities;

public class Owner
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; }
    public string Photo { get; set; }
}
