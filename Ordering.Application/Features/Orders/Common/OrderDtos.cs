namespace Ordering.Application.Features.Orders.Common
{
    public sealed class AddressDto
    {
        public string AddressLine { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string State { get; init; } = string.Empty;
        public string PostalCode { get; init; } = string.Empty;
    }

    public sealed class PaymentDto
    {
        public string? CardName { get; init; }
        public string CardNumber { get; init; } = string.Empty;
        public string Expiration { get; init; } = string.Empty;
        public string CVV { get; init; } = string.Empty;
        public int PaymentMethod { get; init; }
    }

    public sealed class OrderItemDto
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; } = string.Empty;
        public int Quantity { get; init; }
    }

    public sealed class OrderItemInputDto
    {
        public Guid ProductId { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; } = "EGP";
        public int Quantity { get; init; }
    }
}
