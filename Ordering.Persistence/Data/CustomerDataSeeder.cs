using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using Ordering.Persistence.Context;

namespace Ordering.Persistence.Data
{
    public class CustomerDataSeeder
    {
        private readonly ApplicationDbContext _context;

        public CustomerDataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync() {
            await _context.Database.MigrateAsync();
            if (await _context.Customers.AnyAsync())
            {
                return;
            }
            var customers = InitialCustomerData.Customers.Select(c => Customer.Create(c.Id, c.name, Email.Of(c.email)));
            await _context.Customers.AddRangeAsync(customers);
            await _context.SaveChangesAsync();
        }
    }
}
