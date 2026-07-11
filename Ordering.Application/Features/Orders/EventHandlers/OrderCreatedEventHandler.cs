using MediatR;
using Ordering.Domain.Events;

namespace Ordering.Application.Features.Orders.EventHandlers
{
    public sealed class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
