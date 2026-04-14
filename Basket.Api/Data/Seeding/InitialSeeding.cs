using Basket.Api.Data.Entities;
using Marten;
using Marten.Schema;

namespace Basket.Api.Data.Seeding
{
    public class InitialSeeding : IInitialData
    {
        private readonly object[] _data;

        public InitialSeeding(params object[] data)
        {
            _data = data;
        }

        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            await using var session = store.DirtyTrackedSession();
            if (session.Query<ShoppingCart>().Any()) return;
            session.Store(_data);
            await session.SaveChangesAsync(cancellation);
            var carts = session.Query<ShoppingCart>().Any();
            return;
        }
    }

    public static class InitialDataSets
    {
        public static ShoppingCart[] shoppingCarts = new ShoppingCart[] {
            new ShoppingCart
            {
                Username = "john_doe",
                Items = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem { ProductID = Guid.NewGuid(), ProductName = "Product 1", Quantity = 2, Price = 10.0m },
                    new ShoppingCartItem { ProductID = Guid.NewGuid(), ProductName = "Product 2", Quantity = 1, Price = 20.0m }
                }
            },
            new ShoppingCart
            {
                Username = "jane_doe",
                Items = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem { ProductID = Guid.NewGuid(), ProductName = "Product 3", Quantity = 3, Price = 15.0m }
                }
            }
        };
    }
}