using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Million.RealEstate.Backend.Domain.Entities;

public class PropertyImage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string IdProperty { get; set; }

    public string File { get; set; }
    public bool Enabled { get; set; }
}
