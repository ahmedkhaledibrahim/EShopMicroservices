using MediatR;
using Ordering.Application.Abstractions.Persistence;
using Ordering.Application.Features.Orders.Common;
using Ordering.Domain.Aggregates;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = Order.Create(
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

            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(
                    ProductId<Guid>.Of(item.ProductId),
                    Price.Of(item.Price, item.Currency),
                    item.Quantity);
            }

            await _orderRepository.AddAsync(order, cancellationToken);

            return new CreateOrderResponse(order.ID.Value);
        }
    }
}
