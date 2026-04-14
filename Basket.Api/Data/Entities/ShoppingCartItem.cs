namespace Basket.Api.Data.Entities
{
    public class ShoppingCartItem
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
