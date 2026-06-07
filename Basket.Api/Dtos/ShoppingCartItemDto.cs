namespace Basket.Api.Dtos
{
    public class ShoppingCartItemDto
    {
        public Guid ProductID { get; init; }
        public string ProductName { get; set; }
        public int Quantity { get; init; }
        public decimal Price { get; set; }
    }
}
