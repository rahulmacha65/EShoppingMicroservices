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

    public static OrderDto ToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    private static OrderDto DtoFromOrder(this Order order)
    {
        return new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto
                (
                FirstName: order.ShippingAddress.FirstName,
                LastName: order.ShippingAddress.LastName,
                EmailAddress: order.ShippingAddress.EmailAddress,
                AddressLine: order.ShippingAddress.AddressLine,
                State: order.ShippingAddress.State,
                Country: order.ShippingAddress.Country,
                ZipCode: order.ShippingAddress.ZipCode
                ),
                BillingAddress: new AddressDto
                (
                    FirstName: order.BillingAddress.FirstName,
                    LastName: order.BillingAddress.LastName,
                    EmailAddress: order.BillingAddress.EmailAddress,
                    AddressLine: order.BillingAddress.AddressLine,
                    State: order.BillingAddress.State,
                    Country: order.BillingAddress.Country,
                    ZipCode: order.BillingAddress.ZipCode
                ),
                Payment: new PaymentDto
                (
                    CardName: order.Payment.CardName!,
                    CardNumber: order.Payment.CardNumber,
                    Expiration: order.Payment.Expiration,
                    Cvv: order.Payment.CVV,
                    PaymentMethod: order.Payment.PaymentMethod
                ),
                Status: order.Status,
                OrderItems: order.OrderItems.Select(oi => new OrderItemDto
                (
                    OrderId: oi.Id.Value,
                    ProductId: oi.ProductId.Value,
                    Quantity: oi.Quantity,
                    Price: oi.Price
                )).ToList()
            );
    }
}
