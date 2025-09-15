namespace Million.RealEstate.Backend.Application.DTOs;

public class PropertySummaryDto
{
    public string Id { get; set; }
    public string IdOwner { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public Decimal Price { get; set; }
    public string ImageUrl { get; set; }

    public OwnerDto Owner { get; set; }
}
