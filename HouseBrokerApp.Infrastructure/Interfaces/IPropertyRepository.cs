using HouseBrokerApp.Domain.Entities;


namespace HouseBrokerApp.Infrastructure.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property?> GetByIdAsync(Guid id);
        Task<Property> AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Property>> SearchAsync(string location, decimal? minPrice, decimal? maxPrice, string? propertyType);
    }
}
