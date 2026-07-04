using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Persistence.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(c => c.ID).IsRequired().HasConversion(productId => productId.Value, dbProductId => ProductId<Guid>.Of(dbProductId));

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.OwnsOne(oi => oi.Price, priceBuilder =>
            {
                priceBuilder.Property(p => p.Value)
                    .IsRequired()
                    .HasColumnName("Price")
                    .HasColumnType("decimal(10,2)");
                priceBuilder.Property(p => p.Currency)
                    .IsRequired()
                    .HasColumnName("Currency")
                    .HasMaxLength(5);
            });
        }
    }
}
