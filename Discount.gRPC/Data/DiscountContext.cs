using Discount.gRPC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Coupon> Coupons { get; set; }
    }
}
