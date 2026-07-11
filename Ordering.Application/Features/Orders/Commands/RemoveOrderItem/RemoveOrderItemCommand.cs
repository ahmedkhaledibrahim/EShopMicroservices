using MediatR;

namespace Ordering.Application.Features.Orders.Commands.RemoveOrderItem
{
    public sealed class RemoveOrderItemCommand : IRequest<bool>
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
    }
}
