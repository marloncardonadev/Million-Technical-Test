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

    public async Task<List<Owner>> GetAllAsync()
    {
        return await _collection.Find(Builders<Owner>.Filter.Empty).ToListAsync();
    }

    public async Task<Owner?> GetByIdAsync(string id)
    {
        return await _collection.Find(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Owner owner)
    {
        await _collection.InsertOneAsync(owner);
    }

    public async Task UpdateAsync(string id, Owner owner)
    {
        await _collection.ReplaceOneAsync(o => o.Id == id, owner);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(o => o.Id == id);
    }
}
