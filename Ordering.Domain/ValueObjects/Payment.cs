namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string? CardName { get; }
        public string CardNumber { get; }
        public string Expiration { get; }
        public string CVV { get; }
        public int PaymentMethod { get; }

        private Payment(string? CardName, string CardNumber, string Expiration, string CVV, int PaymentMethod)
        {
            this.CardName = CardName;
            this.CardNumber = CardNumber;
            this.Expiration = Expiration;
            this.CVV = CVV;
            this.PaymentMethod = PaymentMethod;
        }

        public static Payment Of(string? CardName, string CardNumber, string Expiration, string CVV, int PaymentMethod) {
            return new Payment(CardName, CardNumber, Expiration, CVV, PaymentMethod);
        }

    }
}
