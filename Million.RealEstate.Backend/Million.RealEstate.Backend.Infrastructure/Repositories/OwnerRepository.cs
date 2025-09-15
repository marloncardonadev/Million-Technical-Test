using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Domain.Entities;
using Million.RealEstate.Backend.Infrastructure.Data;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly IMongoCollection<Owner> _collection;

    public OwnerRepository(PruebaMillionContext context)
    {
        _collection = context.Owners;
    }

    public async Task<Owner?> GetByIdAsync(string id)
    {
        return await _collection.Find(o => o.Id == id).FirstOrDefaultAsync();
    }
}
