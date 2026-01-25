using CMSGTechnical.Domain.Interfaces;
using CMSGTechnical.Mediator.Dtos;
using MediatR;

namespace CMSGTechnical.Mediator.Basket
{
    // Query object representing a request to retrieve a basket by its ID.
    public record GetBasket(int Id) : IRequest<BasketDto>;

    public class GetBasketHandler : IRequestHandler<GetBasket, BasketDto>
    {
        // Repository abstraction for accessing Basket domain models.
        private IRepo<Domain.Models.Basket> Baskets { get; }

        public GetBasketHandler(IRepo<Domain.Models.Basket> baskets)
        {
            // Inject the basket repository.
            Baskets = baskets;
        }

        public async Task<BasketDto> Handle(GetBasket request, CancellationToken cancellationToken)
        {
            // Retrieve the basket from the repository.
            var r = await Baskets.Get(request.Id, cancellationToken);

            // Convert the domain model to a DTO.
            // Warning suppression is used because r may be null,
            // and returning null is acceptable for this query.
#pragma warning disable CS8603 // Possible null reference return.
            return r?.ToDto();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
