using MediatR;
using Ordering.Application.Features.Orders.Common;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommand : IRequest<CreateOrderResponse>
    {
        public Guid CustomerId { get; init; }
        public string OrderName { get; init; } = string.Empty;
        public AddressDto ShippingAddress { get; init; } = null!;
        public AddressDto BillingAddress { get; init; } = null!;
        public PaymentDto Payment { get; init; } = null!;
        public List<OrderItemInputDto> OrderItems { get; init; } = [];
    }
}
