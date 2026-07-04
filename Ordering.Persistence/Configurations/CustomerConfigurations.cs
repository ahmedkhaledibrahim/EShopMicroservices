using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;

namespace Ordering.Persistence.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
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
