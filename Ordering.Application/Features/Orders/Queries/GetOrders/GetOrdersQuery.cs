using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrders
{
    public sealed class GetOrdersQuery : IRequest<IReadOnlyList<OrderResponse>>
    {
        public string? OrderName { get; init; }
        public Guid? CustomerId { get; init; }
    }
}
