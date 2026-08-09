using MediatR;

namespace Basket.Api.Features.Commands.ProcessCheckedoutBasket
{
    public class ProcessCheckedoutBasketRequest : IRequest<Unit>
    {
        public string Username { get; set; }
    }
}
