
namespace Catalog.API.Products.GetProducts;

public record GetProductQuery() : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);

public class GetProductHandler(IDocumentSession session, ILogger<GetProductHandler> logger) : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductHandler.Handle called with query: {@query}", query);

        IEnumerable<Product> products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductResult(products);
    }
}
