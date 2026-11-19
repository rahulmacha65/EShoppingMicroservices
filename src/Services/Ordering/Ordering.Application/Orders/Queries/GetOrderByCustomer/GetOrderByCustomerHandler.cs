
using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public class GetOrderByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
{
    public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders.Include(o => o.OrderItems)
                                           .AsNoTracking()
                                           .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                                           .OrderBy(o => o.OrderName.Value)
                                           .ToListAsync(cancellationToken);

        return new GetOrderByCustomerResult(orders.GetOrderDto());
    }
}
