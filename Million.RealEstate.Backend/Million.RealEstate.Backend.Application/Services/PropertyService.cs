using Million.RealEstate.Backend.Application.Common.Exceptions;
using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Core.DTOs;

namespace Million.RealEstate.Backend.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IPropertyImageRepository _propertyImageRepository;
    private readonly IOwnerRepository _ownerRepository;

    public PropertyService(
        IPropertyRepository propertyRepository,
        IPropertyImageRepository propertyImageRepository,
        IOwnerRepository ownerRepository)
    {
        _propertyRepository = propertyRepository;
        _propertyImageRepository = propertyImageRepository;
        _ownerRepository = ownerRepository;
    }

    public async Task<PropertySummaryDto?> GetByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            var errors = new Dictionary<string, string[]>
            {
                ["Id"] = ["El ID no puede estar vacío"]
            };
            throw new ValidationException(errors);
        }

        var property = await _propertyRepository.GetByIdAsync(id);
        if (property == null) return null;

        var images = await _propertyImageRepository.GetByPropertyIdAsync(property.Id);

        var owner = await _ownerRepository.GetByIdAsync(property.IdOwner);

        PropertySummaryDto propertySummaryDto = new PropertySummaryDto
        {
            Id = property.Id,
            IdOwner = property.IdOwner,
            Name = property.Name,
            Address = property.Address,
            Price = property.Price,
            ImageUrl = string.IsNullOrWhiteSpace(images.FirstOrDefault()?.File) ? "./img/no-image.png" : images.FirstOrDefault()?.File
        };

        if (owner != null)
        {
            propertySummaryDto.Owner = new OwnerDto
            {
                Id = owner.Id,
                Name = owner.Name,
                Address = owner.Address,
                Birthday = owner.Birthday,
                PhotoUrl = string.IsNullOrWhiteSpace(owner.Photo) ? "./img/no-user.png" : owner.Photo
            };
        }

        return propertySummaryDto;
    }

    public async Task<PagedResultDto<PropertySummaryDto>> GetFilteredAsync(PropertyFilterDto filterDto)
    {
        if (filterDto.MinPrice.HasValue && filterDto.MaxPrice.HasValue &&
            filterDto.MinPrice > filterDto.MaxPrice)
        {
            var errors = new Dictionary<string, string[]>
            {
                ["Price"] = ["El precio mínimo no puede ser mayor que el precio máximo"]
            };
            throw new ValidationException(errors);
        }

        if (filterDto.Page < 1) filterDto.Page = 1;
        if (filterDto.Limit < 1 || filterDto.Limit > 100) filterDto.Limit = 10;

        var (properties, totalCount) = await _propertyRepository.GetFilteredAsync(filterDto);

        var items = new List<PropertySummaryDto>();

        foreach (var property in properties)
        {
            var images = await _propertyImageRepository.GetByPropertyIdAsync(property.Id);

            items.Add(new PropertySummaryDto
            {
                Id = property.Id,
                IdOwner = property.IdOwner,
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                ImageUrl = string.IsNullOrWhiteSpace(images.FirstOrDefault()?.File) ? "./img/no-image.png" : images.FirstOrDefault()?.File
            });
        }

        var totalPages = (int)Math.Ceiling(totalCount / (double)filterDto.Limit);

        return new PagedResultDto<PropertySummaryDto>
        {
            Items = items,
            TotalCount = (int)totalCount,
            TotalPages = totalPages,
            CurrentPage = filterDto.Page,
            PageSize = filterDto.Limit
        };
    }
}
