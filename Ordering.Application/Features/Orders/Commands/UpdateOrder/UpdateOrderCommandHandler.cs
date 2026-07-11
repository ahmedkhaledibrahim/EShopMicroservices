using MediatR;
using Ordering.Application.Abstractions.Persistence;
using Ordering.Application.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderId = OrderId<Guid>.Of(request.OrderId);
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken)
                ?? throw new OrderNotFoundException(request.OrderId);

            order.Update(
                CustomerId<Guid>.Of(request.CustomerId),
                OrderName.Of(request.OrderName),
                Address.Of(
                    request.ShippingAddress.AddressLine,
                    request.ShippingAddress.City,
                    request.ShippingAddress.State,
                    request.ShippingAddress.PostalCode),
                Address.Of(
                    request.BillingAddress.AddressLine,
                    request.BillingAddress.City,
                    request.BillingAddress.State,
                    request.BillingAddress.PostalCode),
                Payment.Of(
                    request.Payment.CardName,
                    request.Payment.CardNumber,
                    request.Payment.Expiration,
                    request.Payment.CVV,
                    request.Payment.PaymentMethod));

            await _orderRepository.UpdateAsync(order, cancellationToken);

            return true;
        }
    }
}
