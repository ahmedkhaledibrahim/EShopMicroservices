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

        public override async Task DeleteShoppingCartAsync(string username)
        {
            var cachedCart = await _cacheService.GetAsync<ShoppingCart>(username);
            if (cachedCart != null) { 
              await _cacheService.DeleteAsync<ShoppingCart>(username);
            }
            await _repository.DeleteShoppingCartAsync(username);
        }

        public override async Task<ShoppingCart> GetShoppingCartAsync(string username)
        {
            var cachedCart = await _cacheService.GetAsync<ShoppingCart>(username);
            if(cachedCart != null)
            {
                return cachedCart;
            }
            var cart = await _repository.GetShoppingCartAsync(username);
            if(cart != null) await _cacheService.SetAsync<ShoppingCart>(username, cart, TimeSpan.FromMinutes(5));
            return cart;
        }

        public override async Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart cart)
        {
            await _repository.UpdateShoppingCartAsync(cart);
            await _cacheService.DeleteAsync<ShoppingCart>(cart.Username);
            await _cacheService.SetAsync<ShoppingCart>(cart.Username, cart, TimeSpan.FromMinutes(5));
            return cart;
        }
    }
}
