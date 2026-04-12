using Catalog.Domain.Entities;
using Marten;
using Marten.Internal.Sessions;
using Marten.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.Data
{
    public sealed class InitialSeededData : IInitialData
    {
        private readonly object[] _data;

        public InitialSeededData(params object[] data)
        {
            _data = data;
        }

        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            await using var session = store.DirtyTrackedSession();
            if (await session.Query<Product>().AnyAsync(cancellation)) return;
            session.Store(_data);
            await session.SaveChangesAsync(cancellation);
            var products = session.Query<Product>().AsEnumerable();
        }
    }

    public static class InitialDataSets {
        public static Product[] products = 
        {
            new Product
            {
                ID = Guid.NewGuid(),
                Name = "Product 1",
                Description = "Description",
                Categories = new List<string> { "Category 1", "Category 2" },
                Price = 10.99m,
                ImageFile = "product1.jpg"
            },
            new Product
            {
                ID = Guid.NewGuid(),
                Name = "Product 2",
                Description = "Description",
                Categories = new List<string> { "Category 1", "Category 3" },
                Price = 15.99m,
                ImageFile = "product2.jpg"
            },
            new Product
            {
                ID = Guid.NewGuid(),
                Name = "Product 3",
                Description = "Description",
                Categories = new List<string> { "Category 2", "Category 3" },
                Price = 20.99m,
                ImageFile = "product3.jpg"
            },
            new Product
            {
                ID = Guid.NewGuid(),
                Name = "Product 4",
                Description = "Description",
                Categories = new List<string> { "Category 1", "Category 4" },
                Price = 25.99m,
                ImageFile = "product4.jpg"
            },
            new Product
            {
                ID = Guid.NewGuid(),
                Name = "Product 5",
                Description = "Description",
                Categories = new List<string> { "Category 2", "Category 4" },
                Price = 30.99m,
                ImageFile = "product5.jpg"
            }
        };
    }
}
