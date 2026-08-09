using Basket.Api.Features.Commands.ProcessCheckedoutBasket;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;

namespace Basket.Api.Features.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler : IConsumer<OrderCreatedIntegrationEvent>
    {
        private readonly ISender _sender;

        public OrderCreatedIntegrationEventHandler(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
        {
            await _sender.Send(new ProcessCheckedoutBasketRequest
            {
                Username = context.Message.Username
            });
        }
    }
}
