using Million.RealEstate.Backend.Application.Common.Exceptions;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Domain.Entities;
using Million.RealEstate.Backend.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly IMongoCollection<Owner> _owner;

    public OwnerRepository(PruebaMillionContext owner)
    {
        _owner = owner.Owners;
    }

    public async Task<Owner?> GetByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            var errors = new Dictionary<string, string[]>
            {
                ["Id"] = ["El ID de la propiedad no es válido"]
            };
            throw new ValidationException(errors);
        }
        return await _owner.Find(o => o.Id == id).FirstOrDefaultAsync();
    }
}
