namespace BuildingBlocks.Pagination;

public class PaginationResult<TEntity>(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageIndex;
    public long Count { get; } = count;
    public IEnumerable<TEntity> data { get; } = data;

}
