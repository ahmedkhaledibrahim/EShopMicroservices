using Basket.Api.Application.Exceptions;
using Basket.Api.Data.Entities;
using Basket.Api.Repositories;
using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;
using MediatR;

namespace Basket.Api.Features.Commands.BasketCheckout
{
    public class BasketCheckoutHandler : IRequestHandler<BasketCheckoutRequest, Unit>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketCheckoutHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint)
        {
            _basketRepository = basketRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(BasketCheckoutRequest request, CancellationToken cancellationToken)
        {
            var existingBasket = await _basketRepository.GetShoppingCartAsync(request.Username);
            if (existingBasket == null) throw new BadRequestException("Basket not found for the specified user.");
            if (existingBasket.CheckoutStatus != CheckoutStatus.None) throw new BadRequestException("Basket is already being processed");

            var eventMessage = request.Adapt<BasketCheckoutEvent>();
            eventMessage.Items = existingBasket.Items.Adapt<List<ItemDto>>();
            eventMessage.TotalPrice = existingBasket.TotalPrice;
            
            await _publishEndpoint.Publish(eventMessage, ctx => ctx.SetRoutingKey("basket-checkout"), cancellationToken);
            
            existingBasket.CheckoutStatus = CheckoutStatus.Pending;
            await _basketRepository.UpdateShoppingCartAsync(existingBasket);
            return Unit.Value;
        }
    }
}
