namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product product);
public class GetProductByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));

            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        }).WithName("Get Product By Id")
          .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Gets a product by Id")
          .WithDescription("Gets a product by Id from the postgres database.");
    }
}

