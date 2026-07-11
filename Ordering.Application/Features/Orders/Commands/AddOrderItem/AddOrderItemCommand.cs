using MediatR;
using Ordering.Application.Features.Orders.Common;

namespace Ordering.Application.Features.Orders.Commands.AddOrderItem
{
    public sealed class AddOrderItemCommand : IRequest<bool>
    {
        public Guid OrderId { get; init; }
        public OrderItemInputDto Item { get; init; } = null!;
    }
}
