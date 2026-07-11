using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class Customer : BaseEntity<CustomerId<Guid>>
    {
        private Customer() { }
        public string Name { get; private set; }
        public Email Email { get; private set; }

        public static Customer Create(Guid? Id,string name, Email email) {
            return new Customer
            {
                ID = Id.HasValue ? CustomerId<Guid>.Of(Id.Value) : CustomerId<Guid>.Of(Guid.NewGuid()),
                Email = email,
                Name = name
            };
        }
    }
}
