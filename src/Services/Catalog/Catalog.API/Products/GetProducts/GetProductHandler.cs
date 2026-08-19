
namespace Catalog.API.Products.GetProducts;

public record GetProductQuery() : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);

public class GetProductHandler(IDocumentSession session) : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {


        IEnumerable<Product> products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductResult(products);
    }
}
