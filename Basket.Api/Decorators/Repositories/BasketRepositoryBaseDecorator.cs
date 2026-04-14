using Basket.Api.Data.Entities;
using Basket.Api.Repositories;

namespace Basket.Api.Decorators.Repositories
{
    public abstract class BasketRepositoryBaseDecorator : IBasketRepository
    {
        protected readonly IBasketRepository _repository;

        protected BasketRepositoryBaseDecorator(IBasketRepository repository)
        {
            _repository = repository;
        }

        public abstract Task<ShoppingCart> CreateShoppingCartAsync(ShoppingCart cart);
        public abstract Task DeleteShoppingCartAsync(string username);
        public abstract Task<ShoppingCart> GetShoppingCartAsync(string username);
        public abstract Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart cart);
    }
}
