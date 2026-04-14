using Basket.Api.Data.Entities;
using Basket.Api.Repositories;
using Mapster;
using MediatR;

namespace Basket.Api.Features.Commands.CreateBasket
{
    public class CreateBasketHandler : IRequestHandler<CreateBasketCommand, CreateBasketResponse>
    {
        private readonly IBasketRepository _repository;

        public CreateBasketHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateBasketResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var existingBasket =await _repository.GetShoppingCartAsync(request.Username);
            if (existingBasket != null) { 
               await _repository.DeleteShoppingCartAsync(request.Username);
            }
            TypeAdapterConfig<CreateBasketCommand,ShoppingCart>.NewConfig()
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Items, src => src.Items);

            var newBasket = request.Adapt<ShoppingCart>();
            await _repository.CreateShoppingCartAsync(newBasket);
            return new CreateBasketResponse();
        }
    }
}
