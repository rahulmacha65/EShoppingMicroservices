using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);

public class GetProductHandler(IDocumentSession session) : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        // Removed the null-coalescing operator as PageNumber and PageSize are non-nullable integers
        IEnumerable<Product> products = await session.Query<Product>()
            .ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);

        return new GetProductResult(products);
    }
}
