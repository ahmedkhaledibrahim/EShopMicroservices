using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Basket.Api.Features.Queries.GetBasket
{
    public sealed class GetBasketQuery : IRequest<GetBasketResponse>
    {
        [Required]
        public string Username { get; set; }
    }
}
