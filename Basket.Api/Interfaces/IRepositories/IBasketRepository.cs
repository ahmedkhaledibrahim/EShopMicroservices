using Basket.Api.Data.Entities;

namespace Basket.Api.Repositories
{
    public interface IBasketRepository
    {
        public Task<ShoppingCart> GetShoppingCartAsync(string username);
        public Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart cart);
        public Task DeleteShoppingCartAsync(string username);
        public Task<ShoppingCart> CreateShoppingCartAsync(ShoppingCart cart);
    }
}
