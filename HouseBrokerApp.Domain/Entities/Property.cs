

namespace HouseBrokerApp.Domain.Entities;

public class Property
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? PropertyType { get; set; }
    public string? Location { get; set; }
    public decimal Price { get; set; }
    public string? Features { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? BrokerId { get; set; }
}

