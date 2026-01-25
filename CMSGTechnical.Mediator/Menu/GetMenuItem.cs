using CMSGTechnical.Domain.Interfaces;
using CMSGTechnical.Domain.Models;
using CMSGTechnical.Mediator.Dtos;
using MediatR;

namespace CMSGTechnical.Mediator.Menu
{
    // Query object representing a request to retrieve a single menu item by ID.
    public record GetMenuItem(int Id) : IRequest<MenuItemDto?>;

    public class GetMenuItemHandler : IRequestHandler<GetMenuItem, MenuItemDto?>
    {
        // Repository abstraction for accessing MenuItem domain models.
        private IRepo<MenuItem> MenuItems { get; }

        public GetMenuItemHandler(IRepo<MenuItem> menuItems)
        {
            // Inject the menu item repository.
            MenuItems = menuItems;
        }

        public async Task<MenuItemDto?> Handle(
            GetMenuItem request,
            CancellationToken cancellationToken
        )
        {
            // Retrieve the menu item from the repository.
            var r = await MenuItems.Get(request.Id, cancellationToken);

            // Convert the domain model to a DTO.
            // If r is null, the result will also be null, which is valid for this query.
#pragma warning disable CS8604 // Possible null reference argument.
            return r?.ToDto();
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
