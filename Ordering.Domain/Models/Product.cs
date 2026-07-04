using Ordering.Domain.Abstractions;
using Ordering.Domain.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class Product : BaseEntity<ProductId<Guid>>
    {
        private Product() { }
        public string Name { get; private set; }
        public Price Price { get; private set; }

        public static Product Create(string name, Price price) {
            return new Product
            {
                Name = name,
                Price = price
            };
        }
    }
}
