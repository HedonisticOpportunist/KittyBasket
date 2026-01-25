using CMSGTechnical.Domain.Interfaces;

namespace CMSGTechnical.Domain.Models
{
    // Domain entity representing a menu item in the core business model.
    // Implements IEntity so it can be tracked by the persistence layer (e.g., EF Core).
    public class MenuItem : IEntity
    {
        // Primary key for persistence.
        public int Id { get; set; }

        // Display name of the menu item.
        public string? Name { get; set; }

        // Optional description shown in the UI.
        public string? Description { get; set; }

        // Base price of the item. Domain entities use decimal for currency correctness.
        public decimal Price { get; set; }

        // Quantity is part of the domain model because MenuItems can be nested
        // (e.g., combos, bundles) and may represent structured orders.
        public int Quantity { get; set; } = 1;

        // Category (e.g., "Drinks", "Desserts"). Useful for grouping and sorting.
        public string? Category { get; set; }

        // Optional ordering index for UI or business logic sorting.
        public int Order { get; set; } = 0;

        // Composite relationship: a MenuItem can contain other MenuItems.
        // This supports hierarchical menus (e.g., "Meal Deal" → "Burger", "Fries", "Drink").
        public ICollection<MenuItem> ChildItems { get; set; } = new List<MenuItem>();
    }
}
