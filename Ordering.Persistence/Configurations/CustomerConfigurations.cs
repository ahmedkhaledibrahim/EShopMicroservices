using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Persistence.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(c => c.ID).IsRequired().HasConversion(customerId => customerId.Value, dbCustomerId => CustomerId<Guid>.Of(dbCustomerId));
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.OwnsOne(c => c.Email, emailBuilder =>
            {
                emailBuilder.Property(e => e.Value)
                     .IsRequired()
                     .HasMaxLength(100);
            });
        }
    }
}
