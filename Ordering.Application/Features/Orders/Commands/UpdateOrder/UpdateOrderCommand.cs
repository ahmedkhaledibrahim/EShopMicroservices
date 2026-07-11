using MediatR;
using Ordering.Application.Features.Orders.Common;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; init; }
        public Guid CustomerId { get; init; }
        public string OrderName { get; init; } = string.Empty;
        public AddressDto ShippingAddress { get; init; } = null!;
        public AddressDto BillingAddress { get; init; } = null!;
        public PaymentDto Payment { get; init; } = null!;
    }
}
