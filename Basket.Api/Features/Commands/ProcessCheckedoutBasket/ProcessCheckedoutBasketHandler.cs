using Basket.Api.Data.Entities;
using Basket.Api.Repositories;
using MediatR;

namespace Basket.Api.Features.Commands.ProcessCheckedoutBasket
{
    public class ProcessCheckedoutBasketHandler : IRequestHandler<ProcessCheckedoutBasketRequest, Unit>
    {
        private readonly IBasketRepository _repository;

        public ProcessCheckedoutBasketHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ProcessCheckedoutBasketRequest request, CancellationToken cancellationToken)
        {
            var basket = await _repository.GetShoppingCartAsync(request.Username);
            if (basket != null)
            {
                await _repository.DeleteShoppingCartAsync(basket.Username);
            }
            return Unit.Value;

        }
    }
}
