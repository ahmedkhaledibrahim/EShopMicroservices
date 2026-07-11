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

        public static Product Create(Guid? Id, string name, Price price) {
            return new Product
            {
                ID = Id.HasValue ? ProductId<Guid>.Of(Id.Value) : ProductId<Guid>.Of(Guid.NewGuid()),
                Name = name,
                Price = price
            };
        }
    }
}
