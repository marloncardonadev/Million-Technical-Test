using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Interfaces;

public interface IOwnerService
{
    Task<List<Owner>> GetAllAsync();
    Task<Owner?> GetByIdAsync(string id);
    Task CreateAsync(Owner owner);
    Task UpdateAsync(string id, Owner owner);
    Task DeleteAsync(string id);
}