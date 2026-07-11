using MediatR;
using Ordering.Application.Abstractions.Persistence;
using Ordering.Application.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Features.Orders.Commands.RemoveOrderItem
{
    public sealed class RemoveOrderItemCommandHandler : IRequestHandler<RemoveOrderItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public RemoveOrderItemCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderId = OrderId<Guid>.Of(request.OrderId);
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken)
                ?? throw new OrderNotFoundException(request.OrderId);

            order.RemoveOrderItem(ProductId<Guid>.Of(request.ProductId));

            await _orderRepository.UpdateAsync(order, cancellationToken);

            return true;
        }
    }
}
