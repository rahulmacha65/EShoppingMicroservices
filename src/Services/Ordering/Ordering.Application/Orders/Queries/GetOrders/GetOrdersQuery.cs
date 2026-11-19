using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersQueryResult>;

public record GetOrdersQueryResult(PaginationResult<OrderDto> Orders);
