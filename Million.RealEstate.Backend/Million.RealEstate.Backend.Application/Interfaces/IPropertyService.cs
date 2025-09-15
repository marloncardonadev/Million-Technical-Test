using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Core.DTOs;
using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Interfaces;

public interface IPropertyService
{
    Task<PagedResultDto<PropertySummaryDto>> GetFilteredAsync(PropertyFilterDto filterDto);
    Task<PropertySummaryDto?> GetByIdAsync(string id);
    Task CreateAsync(Property property);
    Task UpdateAsync(string id, Property property);
    Task DeleteAsync(string id);
}
