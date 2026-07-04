using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Persistence.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(oi => oi.ID);
            builder.Property(oi => oi.ID)
                .HasConversion(
                    id => id.Value,
                    dbId => OrderItemId<Guid>.Of(dbId)
                );

            builder.Property(oi => oi.OrderId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    dbId => OrderId<Guid>.Of(dbId)
                );

            builder.Property(oi => oi.ProductId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    dbId => ProductId<Guid>.Of(dbId)
                );

            builder.Property(oi => oi.Quantity)
                .IsRequired();

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
