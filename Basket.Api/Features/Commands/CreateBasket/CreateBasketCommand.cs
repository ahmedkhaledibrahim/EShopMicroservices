using Basket.Api.Dtos;
using MediatR;

namespace Basket.Api.Features.Commands.CreateBasket
{
    public class CreateBasketCommand: IRequest<CreateBasketResponse>
    {
        public string Username { get; init; }
        public List<ShoppingCartItemDto> Items { get; init; }
    }

   
}
