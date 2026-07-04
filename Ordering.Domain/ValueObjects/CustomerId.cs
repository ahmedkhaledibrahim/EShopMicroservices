namespace Ordering.Domain.ValueObjects
{
    public record CustomerId<T>
    {
        public T Value { get; }
        private CustomerId(T value)
        {
            Value = value;
        }

        public static CustomerId<T> Of(T value)
        {
            return new CustomerId<T>(value);
        }
    }
}
