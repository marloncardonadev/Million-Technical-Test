using Million.RealEstate.Backend.Core.DTOs;
using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Interfaces;

public interface IPropertyRepository
{
    Task<(List<Property> Properties, long Total)> GetFilteredAsync(PropertyFilterDto filterDto);
    Task<Property?> GetByIdAsync(string id);
}
