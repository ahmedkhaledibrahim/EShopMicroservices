namespace Ordering.Domain.ValueObjects
{
    public record ProductId<T>
    {
        public T Value { get; }

        private ProductId(T value)
        {
            Value = value;
        }

        public static ProductId<T> Of(T value)
        {
            return new ProductId<T>(value);
        }
    }
}
