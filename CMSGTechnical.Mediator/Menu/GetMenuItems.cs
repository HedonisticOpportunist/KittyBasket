using CMSGTechnical.Domain.Interfaces;
using CMSGTechnical.Domain.Models;
using CMSGTechnical.Mediator.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMSGTechnical.Mediator.Menu
{
    // Query object representing a request to retrieve all menu items.
    public record GetMenuItems : IRequest<IEnumerable<MenuItemDto>>;

    public class GetMenuItemsHandler : IRequestHandler<GetMenuItems, IEnumerable<MenuItemDto>>
    {
        // Repository abstraction for accessing MenuItem domain models.
        private IRepo<MenuItem> MenuItems { get; }

        public GetMenuItemsHandler(IRepo<MenuItem> menuItems)
        {
            // Inject the menu item repository.
            MenuItems = menuItems;
        }

        public async Task<IEnumerable<MenuItemDto>> Handle(
            GetMenuItems request,
            CancellationToken cancellationToken
        )
        {
            // Query all menu items and sort them by price.
            var q = MenuItems.GetAll().OrderBy(m => m.Price);

            // Execute the query against the database.
            var r = await q.ToListAsync(cancellationToken);

            // Convert domain models to DTOs for the caller.
            return r.ToDto();
        }
    }
}
