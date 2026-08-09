using Basket.Api.Data.Entities;
using Basket.Api.Features.Commands.CreateBasket;
using BuildingBlocks.Messaging.Events;
using Mapster;

namespace Basket.Api.MappingConfig
{
    public class BasketMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateBasketCommand, ShoppingCart>()
               .Map(dest => dest.Username, src => src.Username)
               .Map(dest => dest.Items, src => src.Items);
            config.NewConfig<ShoppingCartItem, ItemDto>()
                .Map(dest => dest.ProductId, src => src.ProductID);
        }
    }
}
