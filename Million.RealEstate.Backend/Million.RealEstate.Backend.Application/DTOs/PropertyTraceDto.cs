namespace Million.RealEstate.Backend.Application.DTOs;

public class PropertyTraceDto
{
    public string Id { get; set; }
    public string IdProperty { get; set; }
    public DateTime DateSale { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }
    public double Tax { get; set; }
}
