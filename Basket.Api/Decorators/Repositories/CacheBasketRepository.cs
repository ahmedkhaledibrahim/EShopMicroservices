using Basket.Api.Data.Entities;
using Basket.Api.Interfaces.ICaching;
using Basket.Api.Repositories;

namespace Basket.Api.Decorators.Repositories
{
    public class CacheBasketRepository : BasketRepositoryBaseDecorator
    {
        private readonly ICacheService _cacheService;
        public CacheBasketRepository(IBasketRepository repository, ICacheService cacheService) : base(repository)
        {
            _cacheService = cacheService;
        }

        public override async Task<ShoppingCart> CreateShoppingCartAsync(ShoppingCart cart)
        {
            var createdCart = await _repository.CreateShoppingCartAsync(cart);
            await _cacheService.SetAsync<ShoppingCart>(createdCart.Username, createdCart, TimeSpan.FromMinutes(5));
            return createdCart;
        }

        public override Task DeleteShoppingCartAsync(string username)
        {
            throw new NotImplementedException();
        }

        public override async Task<ShoppingCart> GetShoppingCartAsync(string username)
        {
            var cart = await _repository.GetShoppingCartAsync(username);
            return cart;
        }

        public override Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart cart)
        {
            throw new NotImplementedException();
        }
    }
}
