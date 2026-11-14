using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(cust => cust.Id);
        builder.Property(cust => cust.Id).HasConversion(
            customerId => customerId.Value,
            dbId => CustomerId.Of(dbId));

        builder.Property(cust => cust.Name).IsRequired().HasMaxLength(100);
        builder.Property(cust => cust.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(cust => cust.Email).IsUnique();
    }
}
