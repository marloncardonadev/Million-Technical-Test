namespace Million.RealEstate.Backend.Application.DTOs;

public class PropertyDto
{
    public string Id { get; set; }
    public string IdOwner { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public Decimal Price { get; set; }
    public string CodeInternal { get; set; }
    public int Year { get; set; }
}
