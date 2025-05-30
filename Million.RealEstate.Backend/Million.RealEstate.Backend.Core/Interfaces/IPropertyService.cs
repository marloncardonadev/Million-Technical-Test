using Million.RealEstate.Backend.Core.DTOs;
using Million.RealEstate.Backend.Core.Entities;

namespace Million.RealEstate.Backend.Core.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetProperties();
        Task<PagedResultDto<PropertyDto>> GetFilteredAsync(PropertyFilterDto filterDto);
        Task<PropertyDto?> GetByIdAsync(string id);
    }
}
