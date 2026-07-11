using MediatR;
using Ordering.Application.Abstractions.Persistence;
using Ordering.Application.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderId = OrderId<Guid>.Of(request.OrderId);
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken)
                ?? throw new OrderNotFoundException(request.OrderId);

            await _orderRepository.DeleteAsync(order, cancellationToken);

            return true;
        }
    }
}
