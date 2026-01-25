using CMSGTechnical.Domain.Models;

namespace CMSGTechnical.Mediator.Dtos
{
    // Data Transfer Object representing a menu item as exposed to the UI/application layer.
    public class MenuItemDto
    {
        public int Id { get; set; }

        // Display name of the menu item.
        public string? Name { get; set; }

        // Optional description shown in the UI.
        public string? Description { get; set; }

        // Price of the item (per unit).
        public decimal Price { get; set; }

        // Optional ordering index for sorting in the UI.
        public int Order { get; set; } = 0;

        // Category label (e.g., "Drinks", "Mains", etc.).
        public string? Category { get; set; }

        // Quantity used when the item appears inside a basket.
        public int Quantity { get; set; } = 1;

        // Nested/child menu items (e.g., combo components or add‑ons).
        public ICollection<MenuItemDto> ChildItems { get; set; } = new List<MenuItemDto>();
    }

    public static class MenuItemExtensions
    {
        // Converts a collection of MenuItem domain models into MenuItemDto objects.
        public static IEnumerable<MenuItemDto> ToDto(this IEnumerable<MenuItem> items) =>
            items.Select(i => i.ToDto());

        // Converts a single MenuItem domain model into a MenuItemDto.
        public static MenuItemDto ToDto(this MenuItem menuItem)
        {
            return new MenuItemDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                Order = menuItem.Order,
                Category = menuItem.Category,
                Quantity = menuItem.Quantity,

                // Recursively convert any child items.
                ChildItems = menuItem.ChildItems.ToDto().ToList(),
            };
        }
    }
}
