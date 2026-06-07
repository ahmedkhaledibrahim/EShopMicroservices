using Basket.Api.Data.Entities;
using Basket.Api.Dtos;
using Basket.Api.Repositories;
using Discount.gRPC.Protos;
using Mapster;
using MediatR;
using NetTopologySuite.Index.HPRtree;

namespace Basket.Api.Features.Commands.CreateBasket
{
    public class CreateBasketHandler : IRequestHandler<CreateBasketCommand, CreateBasketResponse>
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public CreateBasketHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _repository = repository;
            _discountProtoService = discountProtoService;
        }

        public async Task<CreateBasketResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var existingBasket =await _repository.GetShoppingCartAsync(request.Username);
            if (existingBasket != null) { 
               await _repository.DeleteShoppingCartAsync(request.Username);
            }
            await CalculateDiscountAmounts(request.Items);
            var newBasket = request.Adapt<ShoppingCart>();
            await _repository.CreateShoppingCartAsync(newBasket);
            return new CreateBasketResponse();
        }

        private async Task CalculateDiscountAmounts(List<ShoppingCartItemDto> items) {
            foreach (var item in items) {
                var discountDetails = await _discountProtoService.GetDiscountAsync(new GetDiscountRequest
                {
                    ProductName = item.ProductName
                });
                if (discountDetails == null) continue;
                item.Price -= discountDetails.Amount;
            }
        }
    }
}
