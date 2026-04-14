using MediatR;

namespace Basket.Api.Features.Commands.CreateBasket
{
    public class CreateBasketCommand: IRequest<CreateBasketResponse>
    {
        public string Username { get; init; }
        public List<ShoppingCartItemDto> Items { get; init; }
    }

    public class ShoppingCartItemDto
    {
        public Guid ProductID { get; init; }
        public string ProductName { get; set; }
        public int Quantity { get; init; }
        public decimal Price { get; init; }
    }
}
