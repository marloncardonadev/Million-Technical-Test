using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Application.Interfaces;

public interface IOwnerRepository
{
    Task<Owner?> GetByIdAsync(string id);
}
