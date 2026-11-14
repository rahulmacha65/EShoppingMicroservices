using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(
                            orderId => orderId.Value,
                            dbId => OrderId.Of(dbId)
                        );

        builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

        builder.HasMany(oi => oi.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(
            o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(name => name.Value)
                           .HasColumnName(nameof(Order.OrderName))
                           .HasMaxLength(100)
                           .IsRequired();
            });

        builder.ComplexProperty(
            o => o.ShippingAddress, shippingaddressBuilder =>
            {
                shippingaddressBuilder.Property(sa => sa.FirstName)
                                      .HasMaxLength(50)
                                      .IsRequired();
                shippingaddressBuilder.Property(sa => sa.LastName)
                                        .HasMaxLength(50)
                                        .IsRequired();
                shippingaddressBuilder.Property(sa => sa.EmailAddress)
                                        .HasMaxLength(50)
                                        .IsRequired();
                shippingaddressBuilder.Property(sa => sa.AddressLine)
                                       .HasMaxLength(200)
                                       .IsRequired();
                shippingaddressBuilder.Property(sa => sa.Country)
                                        .HasMaxLength(50);
                shippingaddressBuilder.Property(sa => sa.State)
                                        .HasMaxLength(50);
                shippingaddressBuilder.Property(sa => sa.ZipCode)
                                        .HasMaxLength(5);
            });

        builder.ComplexProperty(
            o => o.BillingAddress, billingBuilder =>
            {
                billingBuilder.Property(sa => sa.FirstName)
                                      .HasMaxLength(50)
                                      .IsRequired();
                billingBuilder.Property(sa => sa.LastName)
                                        .HasMaxLength(50)
                                        .IsRequired();
                billingBuilder.Property(sa => sa.EmailAddress)
                                        .HasMaxLength(50)
                                        .IsRequired();
                billingBuilder.Property(sa => sa.AddressLine)
                                       .HasMaxLength(200)
                                       .IsRequired();
                billingBuilder.Property(sa => sa.Country)
                                        .HasMaxLength(50);
                billingBuilder.Property(sa => sa.State)
                                        .HasMaxLength(50);
                billingBuilder.Property(sa => sa.ZipCode)
                                        .HasMaxLength(5);
            });

        builder.ComplexProperty(
            o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardNumber)
                              .HasMaxLength(24)
                               .IsRequired();
                paymentBuilder.Property(p => p.CardName)
                              .HasMaxLength(50);
                paymentBuilder.Property(p => p.Expiration)
                              .IsRequired();
                paymentBuilder.Property(p => p.CVV)
                              .HasMaxLength(3);
                paymentBuilder.Property(p => p.PaymentMethod)
                              .IsRequired();
            });

        builder.Property(s => s.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                   status => status.ToString(),
                   dbStatus => Enum.Parse<OrderStatus>(dbStatus));

        builder.Property(o => o.TotalPrice);
    }
}
