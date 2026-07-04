using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Aggregates;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Persistence.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(o => o.ID).HasConversion(orderId => orderId.Value, dbId => OrderId<Guid>.Of(dbId));

            builder.Property(o => o.OrderName).IsRequired().HasMaxLength(200).HasConversion(orderName => orderName.Value, dbOrderName => OrderName.Of(dbOrderName));

            builder.Property(o => o.CustomerId)
           .IsRequired()
           .HasConversion(
               id => id.Value,
               dbId => CustomerId<Guid>.Of(dbId)
           );

            builder.HasOne<Customer>()         
                .WithMany()      
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.Property(o => o.Status)
            .IsRequired()
            .HasDefaultValue(OrderStatus.Pending)
            .HasConversion<string>();

            builder.Ignore(o => o.TotalPrice);

            builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.AddressLine).IsRequired().HasMaxLength(200);
                addressBuilder.Property(a => a.State).HasMaxLength(50);
                addressBuilder.Property(a => a.City).HasMaxLength(50);
                addressBuilder.Property(a => a.PostalCode).HasMaxLength(50);
            });

            builder.OwnsOne(o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.AddressLine).IsRequired().HasMaxLength(200);
                addressBuilder.Property(a => a.State).HasMaxLength(50);
                addressBuilder.Property(a => a.City).HasMaxLength(50);
                addressBuilder.Property(a => a.PostalCode).HasMaxLength(50);
            });

            builder.OwnsOne(o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName).HasMaxLength(50);
                paymentBuilder.Property(p => p.CardNumber).IsRequired().HasMaxLength(24);
                paymentBuilder.Property(p => p.Expiration).IsRequired().HasMaxLength(10);
                paymentBuilder.Property(p => p.CVV).IsRequired().HasMaxLength(3);
                paymentBuilder.Property(p => p.PaymentMethod).IsRequired();
            }); 
        }
    }
}
