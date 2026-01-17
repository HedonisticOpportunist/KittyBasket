using CMSGTechnical.Domain.Interfaces;
using CMSGTechnical.Mediator.Dtos;
using MediatR;

namespace CMSGTechnical.Mediator.Basket
{
    public record GetBasket(int Id) : IRequest<BasketDto>;

    public class GetBasketHandler : IRequestHandler<GetBasket, BasketDto>
    {
        private IRepo<Domain.Models.Basket> Baskets { get; }

        public GetBasketHandler(IRepo<Domain.Models.Basket> baskets)
        {
            Baskets = baskets;
        }

        public async Task<BasketDto> Handle(GetBasket request, CancellationToken cancellationToken)
        {
            var r = await Baskets.Get(request.Id, cancellationToken);
#pragma warning disable CS8603 // Possible null reference return.
            return r?.ToDto();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
