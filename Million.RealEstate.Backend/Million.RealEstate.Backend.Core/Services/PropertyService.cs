using Million.RealEstate.Backend.Core.DTOs;
using Million.RealEstate.Backend.Core.Entities;
using Million.RealEstate.Backend.Core.Interfaces;

namespace Million.RealEstate.Backend.Core.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<Property>> GetProperties()
        {
            var properties = await _propertyRepository.GetProperties();

            return properties;
        }

        public async Task<PagedResultDto<PropertyDto>> GetFilteredAsync(PropertyFilterDto filterDto)
        {
            if (filterDto.MinPrice.HasValue && filterDto.MaxPrice.HasValue && filterDto.MinPrice > filterDto.MaxPrice)
            {
                throw new ArgumentException("El precio mínimo no puede ser mayor que el precio máximo.");
            }

            if (filterDto.Page < 1)
            {
                filterDto.Page = 1;
            }

            if (filterDto.Limit < 1 || filterDto.Limit > 100)
            {
                filterDto.Limit = 10;
            }

            var properties = await _propertyRepository.GetFilteredAsync(filterDto);

            return properties;
        }

        public async Task<PropertyDto?> GetByIdAsync(string id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);

            return property;
        }
    }
}
