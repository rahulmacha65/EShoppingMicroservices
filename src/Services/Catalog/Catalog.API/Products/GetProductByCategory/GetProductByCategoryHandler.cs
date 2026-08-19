
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string category) : IRequest<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> product);

public class GetProductByCategoryHandler(IDocumentSession session) :
    IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {

        var result = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(result);
    }
}

