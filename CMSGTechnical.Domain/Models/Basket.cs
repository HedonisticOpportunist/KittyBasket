using CMSGTechnical.Domain.Interfaces;

namespace CMSGTechnical.Domain.Models
{
    // Domain entity representing a user's basket in the core business model.
    // Implements IEntity so it can be tracked by persistence (e.g., EF Core).
    public class Basket : IEntity
    {
        // Primary key for persistence.
        public int Id { get; set; }

        // Collection of MenuItem domain entities.
        // These are full domain objects, not DTOs.
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        // The user who owns this basket.
        // In a real system this might be a navigation property.
        public int UserId { get; set; }
    }
}
