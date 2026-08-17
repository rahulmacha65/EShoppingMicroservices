
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string category) : IRequest<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> product);

public class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger) :
    IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProductByCategoryQuery for category: {Category}", query.category);

        var result = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(result);
    }
}

