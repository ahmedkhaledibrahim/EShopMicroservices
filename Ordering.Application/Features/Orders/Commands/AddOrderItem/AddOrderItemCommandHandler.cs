using MediatR;
using Ordering.Application.Abstractions.Persistence;
using Ordering.Application.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Features.Orders.Commands.AddOrderItem
{
    public sealed class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public AddOrderItemCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderId = OrderId<Guid>.Of(request.OrderId);
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken)
                ?? throw new OrderNotFoundException(request.OrderId);

            order.AddOrderItem(
                ProductId<Guid>.Of(request.Item.ProductId),
                Price.Of(request.Item.Price, request.Item.Currency),
                request.Item.Quantity);

            await _orderRepository.UpdateAsync(order, cancellationToken);

            return true;
        }
    }
}
