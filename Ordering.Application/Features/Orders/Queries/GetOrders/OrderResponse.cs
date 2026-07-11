using Ordering.Application.Features.Orders.Common;

namespace Ordering.Application.Features.Orders.Queries.GetOrders
{
    public sealed class OrderResponse
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public string OrderName { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public decimal TotalPrice { get; init; }
        public AddressDto ShippingAddress { get; init; } = null!;
        public AddressDto BillingAddress { get; init; } = null!;
        public PaymentDto Payment { get; init; } = null!;
        public List<OrderItemDto> OrderItems { get; init; } = [];
    }
}
