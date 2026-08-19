namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product product);

public class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {

        var result = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (result == null)
        {
            throw new ProductNotFoundException(query.Id);
        }
        else
        {
            return new GetProductByIdResult(result);
        }
    }
}
