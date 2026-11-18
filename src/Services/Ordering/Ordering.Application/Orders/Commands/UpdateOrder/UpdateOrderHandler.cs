
using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        //update order entity from command object
        OrderId orderId = OrderId.Of(command.orderDto.Id);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.orderDto.Id);
        }

        UpdateOrder(order, command.orderDto);

        // Save changes to the database
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Return the result
        return new UpdateOrderResult(true);
    }

    private void UpdateOrder(Order order, OrderDto orderDto)
    {
        Address updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
        Address updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
        Payment updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

        order.Update(OrderName.Of(orderDto.OrderName),
                          shippingAddress: updatedShippingAddress,
                          billingAddress: updatedBillingAddress,
                            payment: updatedPayment,
                            status: orderDto.Status);

    }
}
