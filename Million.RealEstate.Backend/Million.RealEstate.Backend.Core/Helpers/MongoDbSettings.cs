namespace Million.RealEstate.Backend.Core.Helpers
{
    public class MongoDbSettings
    {
        public string? ConnectionString { get; set; } = null!;
        public string? DatabaseName { get; set; } = null!;
        public string? CollectionName { get; set; } = null!;
    }
}
