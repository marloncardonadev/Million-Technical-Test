using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Core.DTOs;

namespace Million.RealEstate.Backend.Application.Interfaces;

public interface IPropertyService
{
    Task<PagedResultDto<PropertySummaryDto>> GetFilteredAsync(PropertyFilterDto filterDto);
    Task<PropertySummaryDto?> GetByIdAsync(string id);
}
