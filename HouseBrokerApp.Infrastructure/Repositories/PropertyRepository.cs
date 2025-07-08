

using HouseBrokerApp.Domain.Entities;
using HouseBrokerApp.Infrastructure.Data;
using HouseBrokerApp.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HouseBrokerApp.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly HouseBrokerDbContext _context;
        public PropertyRepository(HouseBrokerDbContext context)
        {
            _context = context;
        }
        public async Task<Property> AddAsync(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task DeleteAsync(Guid id)
        {
            var property = await _context.Properties.FindAsync(id);
            if(property != null)
            {
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _context.Properties.ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(Guid id)
        {
            return await _context.Properties.FindAsync(id);
        }

        public async Task<IEnumerable<Property>> SearchAsync(string location, decimal? minPrice, decimal? maxPrice, string? propertyType)
        {
            var query = _context.Properties.AsQueryable();

            if (!string.IsNullOrEmpty(location))
                query = query.Where(p => p.Location.Contains(location));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice);

            if (!string.IsNullOrEmpty(propertyType))
                query = query.Where(p => p.PropertyType == propertyType);

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }
    }
}
