namespace CMSGTechnical.Mediator.Dtos
{
    // Data Transfer Object representing a basket as exposed to the application/UI layer.
    public class BasketDto
    {
        public int Id { get; set; }

        // Collection of menu items in the basket.
        // Initialized to an empty list to avoid null references.
        public ICollection<MenuItemDto> MenuItems { get; set; } = new List<MenuItemDto>();

        // The ID of the user who owns this basket.
        public int UserId { get; set; }
    }

    public static class BasketExtensions
    {
        // Converts a collection of Basket domain models into BasketDto objects.
        public static IEnumerable<BasketDto> ToDto(this IEnumerable<Domain.Models.Basket> models) =>
            models.Select(i => i.ToDto()).ToArray();

        // Converts a single Basket domain model into a BasketDto.
        public static BasketDto ToDto(this Domain.Models.Basket model)
        {
            return new BasketDto
            {
                Id = model.Id,
                MenuItems = model.MenuItems.ToDto().ToList(), // Convert nested items as well.
                UserId = model.UserId,
            };
        }
    }
}
