using Basket.Api.Data.Entities;
using Basket.Api.Features.Commands.CreateBasket;
using Mapster;

namespace Basket.Api.MappingConfig
{
    public class BasketMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<CreateBasketCommand, ShoppingCart>.NewConfig()
               .Map(dest => dest.Username, src => src.Username)
               .Map(dest => dest.Items, src => src.Items);
        }
    }
}
