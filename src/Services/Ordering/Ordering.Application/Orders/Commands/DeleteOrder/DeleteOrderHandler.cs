using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        //find the order by id
        Order? order = await dbContext.Orders.FindAsync([OrderId.Of(command.Id)], cancellationToken);
        if (order == null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        //save change to the database
        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        //return the result
        return new DeleteOrderResult(true);
    }
}
