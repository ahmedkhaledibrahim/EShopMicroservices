namespace Ordering.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            Value = value;
        }
        public static Email Of(string value) {
            return new Email(value);
        }
    }
}
