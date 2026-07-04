namespace Ordering.Domain.ValueObjects
{
    public record Price
    {
        public decimal Value { get; }
        public string Currency { get; }

        private Price(decimal value, string currency)
        {
            Value = value;
            Currency = currency;
        }

        public static Price Of(decimal value, string currency)
        {
            if (value < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }

            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentException("Currency cannot be empty.");
            }

            return new Price(value, currency);
        }
    }
}