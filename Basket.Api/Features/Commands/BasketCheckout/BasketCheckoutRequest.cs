using MediatR;

namespace Basket.Api.Features.Commands.BasketCheckout
{
    public class BasketCheckoutRequest : IRequest<Unit>
    {
        public string Username { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalPrice { get; set; }

        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public string? CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}
