using Basket.Api.Repositories;
using Mapster;
using MediatR;

namespace Basket.Api.Features.Queries.GetBasket
{
    public sealed class GetBasketHandler : IRequestHandler<GetBasketQuery, GetBasketResponse>
    {
        private readonly IBasketRepository _repository;

        public GetBasketHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetBasketResponse> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await _repository.GetShoppingCartAsync(request.Username);
            if (basket == null) return null;
            var response = basket.Adapt<GetBasketResponse>();
            return response;
        }
    }
}
