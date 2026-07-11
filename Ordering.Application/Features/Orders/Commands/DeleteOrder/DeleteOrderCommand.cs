using MediatR;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; init; }
    }
}
