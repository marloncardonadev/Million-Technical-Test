using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Services;

public class OwnerService : IOwnerService
{
    public Task CreateAsync(Owner owner)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Owner>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Owner?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(string id, Owner owner)
    {
        throw new NotImplementedException();
    }
}
