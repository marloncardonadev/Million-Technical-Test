using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Million.RealEstate.Backend.Core.Entities
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null;
        [BsonElement("idOwner")]
        public string? IdOwner { get; set; } = null;
        [BsonElement("name")]
        public string? Name { get; set; } = null;
        [BsonElement("address")]
        public string? Address { get; set; } = null;
        [BsonElement("price")]
        public Decimal Price { get; set; }
        [BsonElement("imageUrl")]
        public string? ImageUrl { get; set; } = null;
    }
}
