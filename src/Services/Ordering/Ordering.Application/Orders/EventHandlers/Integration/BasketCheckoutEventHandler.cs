using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckOutEvent> logger) : IConsumer<BasketCheckOutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckOutEvent> context)
    {
        //Create new order and start order full fillment process
        logger.LogInformation("Integeration event hanlder: {IntegrationEvent}", context.Message.GetType().Name);

        CreateOrderCommand command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);

    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckOutEvent message)
    {
        AddressDto addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        PaymentDto paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
        Guid orderId = Guid.NewGuid();

        OrderDto orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: Ordering.Domain.Enums.OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId, new Guid("63aec511-cc98-43a6-98e4-40d08426cd4a"),2,10000),
                new OrderItemDto(orderId, new Guid("d77b6ed3-fe17-40a6-b43c-989e5093deb2"),2,800)
            ]
            );
        return new CreateOrderCommand(orderDto);
    }
}
