using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class Customer : BaseEntity<CustomerId<Guid>>
    {
        private Customer() { }
        public string Name { get; private set; }
        public Email Email { get; private set; }

        public static Customer Create(string name, Email email) {
            return new Customer
            {
                Email = email,
                Name = name
            };
        }
    }
}
