namespace Ordering.Domain.ValueObjects
{
    public record OrderItemId<T>
    {
       
        public T Value { get; }
         private OrderItemId(T Value)
        {
            this.Value = Value;
        }
        public static OrderItemId<T> Of(T value)
        {
            return new OrderItemId<T>(value);
        }
    }
}
