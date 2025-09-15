namespace Million.RealEstate.Backend.Core.DTOs;

public class PropertyFilterDto
{
    public string? IdOwner { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
    public string SortBy { get; set; } = "name";
    public string SortDirection { get; set; } = "asc";
}
