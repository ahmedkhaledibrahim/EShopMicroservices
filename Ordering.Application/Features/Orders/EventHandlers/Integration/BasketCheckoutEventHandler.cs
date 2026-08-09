using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CreateOrder;

namespace Ordering.Application.Features.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler : IConsumer<BasketCheckoutEvent>
    {
        private readonly ISender _sender;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketCheckoutEventHandler(ISender sender, IPublishEndpoint publishEndpoint)
        {
            _sender = sender;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = context.Message.Adapt<CreateOrderCommand>();
            await _sender.Send(command);
            await _publishEndpoint.Publish(new OrderCreatedIntegrationEvent
            {
                Username = context.Message.Username
            });
        }
    }
}
