using Discount.gRPC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { ID = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
                new Coupon { ID = 2, ProductName = "Samsung S10", Description = "Samsung Discount", Amount = 100 }
            );
        }
    }
}
