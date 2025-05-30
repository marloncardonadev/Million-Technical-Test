namespace Million.RealEstate.Backend.Core.DTOs
{
    public class PropertyDto
    {
        public string? IdOwner { get; set; } = null;
        public string? Name { get; set; } = null;
        public string? Address { get; set; } = null;
        public Decimal Price { get; set; }
        public string? ImageUrl { get; set; } = null;
    }
}
