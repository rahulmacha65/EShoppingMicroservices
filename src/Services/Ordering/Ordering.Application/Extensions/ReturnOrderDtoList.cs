namespace Ordering.Application.Extensions;

public static class ReturnOrderDtoList
{
    public static IEnumerable<OrderDto> GetOrderDto(this IEnumerable<Order> orders)
    {
        return orders.Select(o => new OrderDto
        (
            Id: o.Id.Value,
            CustomerId: o.CustomerId.Value,
            OrderName: o.OrderName.Value,
            ShippingAddress: new AddressDto
            (
                FirstName: o.ShippingAddress.FirstName,
                LastName: o.ShippingAddress.LastName,
                EmailAddress: o.ShippingAddress.EmailAddress,
                AddressLine: o.ShippingAddress.AddressLine,
                State: o.ShippingAddress.State,
                Country: o.ShippingAddress.Country,
                ZipCode: o.ShippingAddress.ZipCode
            ),
            BillingAddress: new AddressDto
            (
                FirstName: o.BillingAddress.FirstName,
                LastName: o.BillingAddress.LastName,
                EmailAddress: o.BillingAddress.EmailAddress,
                AddressLine: o.BillingAddress.AddressLine,
                State: o.BillingAddress.State,
                Country: o.BillingAddress.Country,
                ZipCode: o.BillingAddress.ZipCode
            ),
            Payment: new PaymentDto
            (
                CardName: o.Payment.CardName!,
                CardNumber: o.Payment.CardNumber,
                Expiration: o.Payment.Expiration,
                Cvv: o.Payment.CVV,
                PaymentMethod: o.Payment.PaymentMethod
            ),
            Status: o.Status,
            OrderItems: o.OrderItems.Select(oi => new OrderItemDto
            (
                OrderId: oi.Id.Value,
                ProductId: oi.ProductId.Value,
                Quantity: oi.Quantity,
                Price: oi.Price
            )).ToList()
        ));
    }
}
