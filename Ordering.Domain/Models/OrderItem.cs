using Ordering.Domain.Abstractions;
using Ordering.Domain.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class OrderItem : BaseEntity<OrderItemId<Guid>>
    {
        private OrderItem() { }
        public OrderItem(ProductId<Guid> productId, OrderId<Guid> orderId, Price price, int quantity)
        {
            ID = OrderItemId<Guid>.Of(Guid.NewGuid());
            ProductId = productId;
            OrderId = orderId;
            Price = price;
            Quantity = quantity;
        }

        public ProductId<Guid> ProductId { get; private set; }
        public OrderId<Guid> OrderId { get; private set; }
        public Price Price { get;  private set; }
        public int Quantity { get; private set; }
        public static OrderItem Create(ProductId<Guid> productId, OrderId<Guid> orderId,
                                   Price price, int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            if (price.Value <= 0)
                throw new DomainException("Price must be greater than zero.");

            return new OrderItem(productId, orderId, price, quantity);
        }
    }
}
