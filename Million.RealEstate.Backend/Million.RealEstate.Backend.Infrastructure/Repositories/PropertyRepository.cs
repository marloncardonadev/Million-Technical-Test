using Million.RealEstate.Backend.Core.DTOs;
using Million.RealEstate.Backend.Core.Entities;
using Million.RealEstate.Backend.Core.Interfaces;
using Million.RealEstate.Backend.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoCollection<Property> _collection;

        public PropertyRepository(PruebaMillionContext context)
        {
            _collection = context.Properties;
        }

        public async Task<IEnumerable<Property>> GetProperties()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<PagedResultDto<PropertyDto>> GetFilteredAsync(PropertyFilterDto filterDto)
        {
            var filter = Builders<Property>.Filter.Empty;

            if (!string.IsNullOrEmpty(filterDto.IdOwner))
            {
                filter &= Builders<Property>.Filter.Eq(p => p.IdOwner, filterDto.IdOwner);
            }

            if (!string.IsNullOrEmpty(filterDto.Name))
            {
                filter &= Builders<Property>.Filter.Regex(p => p.Name, new BsonRegularExpression(filterDto.Name, "i"));
            }

            if (!string.IsNullOrEmpty(filterDto.Address))
            {
                filter &= Builders<Property>.Filter.Regex(p => p.Address, new BsonRegularExpression(filterDto.Address, "i"));
            }

            if (filterDto.MinPrice.HasValue)
            {
                filter &= Builders<Property>.Filter.Gte(p => p.Price, filterDto.MinPrice.Value);
            }

            if (filterDto.MaxPrice.HasValue)
            {
                filter &= Builders<Property>.Filter.Lte(p => p.Price, filterDto.MaxPrice.Value);
            }

            var sortBuilder = Builders<Property>.Sort;
            var sort = filterDto.SortDirection.ToLower() == "desc"
                ? sortBuilder.Descending(filterDto.SortBy)
                : sortBuilder.Ascending(filterDto.SortBy);

            int skip = (filterDto.Page - 1) * filterDto.Limit;

            var total = await _collection.CountDocumentsAsync(filter);

            var items = await _collection.Find(filter)
                .Sort(sort)
                .Skip(skip)
                .Limit(filterDto.Limit)
                .Project(p => new PropertyDto
                {
                    IdOwner = p.IdOwner,
                    Name = p.Name,
                    Address = p.Address,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(total / (double)filterDto.Limit);

            return new PagedResultDto<PropertyDto>
            {
                Items = items,
                TotalCount = (int)total,
                TotalPages = totalPages,
                CurrentPage = filterDto.Page,
                PageSize = filterDto.Limit
            };
        }

        public async Task<PropertyDto?> GetByIdAsync(string id)
        {
            var filter = Builders<Property>.Filter.Eq(p => p.IdOwner, id);

            return await _collection
                .Find(filter)
                .Project(p => new PropertyDto
                {
                    IdOwner = p.IdOwner,
                    Name = p.Name,
                    Address = p.Address,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                })
                .FirstOrDefaultAsync();
        }
    }
}
