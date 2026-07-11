
using Ordering.Domain.Abstractions;

namespace Ordering.Domain.ValueObjects
{
    public record OrderId<T> : IIdentifier
    {
        public T Value { get; }

        private OrderId(T value)
        {
            Value = value;
        }

        public static OrderId<T> Of(T value)
        {
            return new OrderId<T>(value);
        }
    }
}
