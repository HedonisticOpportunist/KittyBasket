using CMSGTechnical.Domain.Interfaces;
using CMSGTechnical.Domain.Models;
using CMSGTechnical.Mediator.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMSGTechnical.Mediator.Menu
{
    public record GetMenuItems : IRequest<IEnumerable<MenuItemDto>>;

    public class GetMenuItemsHandler : IRequestHandler<GetMenuItems, IEnumerable<MenuItemDto>>
    {

        private IRepo<MenuItem> MenuItems { get; }

        public GetMenuItemsHandler(IRepo<MenuItem> menuItems)
        {
            MenuItems = menuItems;
        }

        public async Task<IEnumerable<MenuItemDto>> Handle(GetMenuItems request, CancellationToken cancellationToken)
        {
            var q = MenuItems.GetAll().OrderBy(m => m.Price); // Sort menu items by price. 
            var r = await q.ToListAsync(cancellationToken);
            return r.ToDto();
        }
    }



}
