using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.Events;
using Ordering.Domain.Exceptions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Aggregates
{
    public class Order : Aggregate<OrderId<Guid>>
    {
        //This is for EF core, and to not let anyone be able to create an instance from the class
        private Order()
        {
            
        }
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerId<Guid> CustomerId { get; private set; } = default;
        public OrderName OrderName { get; private set; } = default;
        public Address ShippingAddress { get; private set; } = default;
        public Address BillingAddress { get; private set; } = default;
        public Payment Payment { get; private set; } = default;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice =>  OrderItems.Sum(x => x.Price.Value * x.Quantity);

        public void AddOrderItem(ProductId<Guid> productId, Price price, int quantity) {
            if (_orderItems.Any(x => x.ProductId == productId))
                throw new DomainException("Product already exists in this order.");

            var item = OrderItem.Create(productId,this.ID, price, quantity);

            _orderItems.Add(item);
        }

        public void RemoveOrderItem(ProductId<Guid> productId) {
            var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem != null) {
                _orderItems.Remove(orderItem);
            }
        }

        public static Order Create(CustomerId<Guid> customerId,
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment) {
            var order = new Order
            {
                ID = OrderId<Guid>.Of(Guid.NewGuid()),
                OrderName = orderName,
                CustomerId = customerId,
                Payment = payment,
                BillingAddress = billingAddress,
                ShippingAddress = shippingAddress
            };
            
            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }
        
    }
}
                                                                    