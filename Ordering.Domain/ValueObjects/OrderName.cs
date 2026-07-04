namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        public string Value { get; }
        private OrderName(string value)
        {
            Value = value;
        }

        public static OrderName Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Order name cannot be empty.");

            if (value.Length > 100)
                throw new ArgumentException("Order name cannot exceed 100 characters.");

            return new OrderName(value);
        }
    }
}
