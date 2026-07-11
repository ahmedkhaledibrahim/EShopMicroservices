using Mapster;
using MediatR;
using Ordering.Application.Abstractions.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrders
{
    public sealed class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IReadOnlyList<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IReadOnlyList<OrderResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAsync(request.OrderName, request.CustomerId, cancellationToken);

            return orders.Adapt<List<OrderResponse>>();
        }
    }
}