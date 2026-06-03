using Discount.gRPC.Domain.Entities;
using Discount.gRPC.Protos;
using Mapster;

namespace Discount.gRPC.MappingConfig
{
    public class CouponsMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateDiscountRequest, Coupon>().Map(dest => dest.ID, src => src.Id).IgnoreNullValues(true);
        }
    }
}
