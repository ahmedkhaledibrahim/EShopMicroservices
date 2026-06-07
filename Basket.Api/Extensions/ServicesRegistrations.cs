using Basket.Api.Decorators.Repositories;
using Basket.Api.Implementations.Caching;
using Basket.Api.Implementations.Repositories;
using Basket.Api.Interfaces.ICaching;
using Basket.Api.Repositories;
using Discount.gRPC.Protos;
using Mapster;
using System.Reflection;

namespace Basket.Api.Extensions
{
    public static class ServicesRegistrations
    {
        public static void AddServices(this IServiceCollection services) {
            TypeAdapterConfig.GlobalSettings.Scan(typeof(IRegister).Assembly);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddScoped<ICacheService, RedisCache>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.Decorate<IBasketRepository, CacheBasketRepository>();
        }

        public static void AddGrpcClientConfigurations(this IServiceCollection services, IConfiguration configuration) {
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options => {
                options.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]!);
            });
        }
    }
}
