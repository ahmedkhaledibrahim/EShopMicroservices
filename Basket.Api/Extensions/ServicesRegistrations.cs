using Basket.Api.Decorators.Repositories;
using Basket.Api.Implementations.Caching;
using Basket.Api.Implementations.Repositories;
using Basket.Api.Interfaces.ICaching;
using Basket.Api.Repositories;
using System.Reflection;

namespace Basket.Api.Extensions
{
    public static class ServicesRegistrations
    {
        public static void AddServices(this IServiceCollection services) {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddScoped<ICacheService, RedisCache>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.Decorate<IBasketRepository, CacheBasketRepository>();
        }
    }
}
