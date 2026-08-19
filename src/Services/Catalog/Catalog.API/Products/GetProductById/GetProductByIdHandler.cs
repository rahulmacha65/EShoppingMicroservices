namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product product);

public class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProductByIdQuery for Id: {Id}", query.Id);

        var result = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (result == null)
        {
            logger.LogWarning("Product with Id: {Id} not found", query.Id);
            throw new ProductNotFoundException(query.Id);
        }
        else
        {
            logger.LogInformation("Product with Id: {Id} found", query.Id);
            return new GetProductByIdResult(result);
        }
    }
}
