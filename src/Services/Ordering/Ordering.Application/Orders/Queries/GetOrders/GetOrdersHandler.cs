using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>
{
    public async Task<GetOrdersQueryResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        int pageSize = query.PaginationRequest.PageSize;
        int pageIndex = query.PaginationRequest.PageIndex;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders.Include(o => o.OrderItems)
                               .OrderBy(o => o.OrderName.Value)
                               .Skip(pageSize * pageIndex)
                               .Take(pageIndex)
                               .ToListAsync(cancellationToken);



        IEnumerable<OrderDto> orderDto = orders.GetOrderDto();

        return new GetOrdersQueryResult(new PaginationResult<OrderDto>(pageIndex, pageSize, totalCount, orderDto));
    }
}
