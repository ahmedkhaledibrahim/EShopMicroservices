using Marten.Schema;

namespace Basket.Api.Data.Entities
{
    public class ShoppingCart
    {
        [Identity]
        public string Username { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
        public CheckoutStatus CheckoutStatus { get; set; } = CheckoutStatus.None;
        public DateTime? CheckoutInitiatedAt { get; set; }

    }

    public enum CheckoutStatus {
        None = 0,
        Pending = 1,
        Completed = 2,
        Failed = 3
    }
}
