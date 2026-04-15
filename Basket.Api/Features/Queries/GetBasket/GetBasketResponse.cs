using Basket.Api.Dtos;

namespace Basket.Api.Features.Queries.GetBasket
{
    public sealed class GetBasketResponse
    {
        public string Username { get; init; }
        public decimal TotalPrice => Items.Sum(x => x.Quantity * x.Price);
        public List<ShoppingCartItemDto> Items { get; init; }
    }
}
