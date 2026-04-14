using Basket.Api.Data.Entities;
using Basket.Api.Repositories;
using Marten;

namespace Basket.Api.Implementations.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDocumentSession _session;

        public BasketRepository(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<ShoppingCart> CreateShoppingCartAsync(ShoppingCart cart)
        {
            _session.Store(cart);
            await _session.SaveChangesAsync();
            return cart;
        }

        public async Task DeleteShoppingCartAsync(string username)
        {
            _session.DeleteWhere<ShoppingCart>(c => c.Username == username);
            await _session.SaveChangesAsync();
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string username)
        {
            var cart = await _session.Query<ShoppingCart>().FirstOrDefaultAsync(c => c.Username == username);
            return cart;
        }

        public async Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart cart)
        {
            _session.Update(cart);
            await _session.SaveChangesAsync();
            return cart;
        }
    }
}
