namespace Catalog.API.Products.GetProducts;

public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getProducts", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductQuery());

            var response = result.Adapt<GetProductResponse>();

            return Results.Ok(response);
        }).WithName("Get all Products")
          .Produces<GetProductResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .ProducesProblem(StatusCodes.Status404NotFound)
          .WithSummary("Gets all products")
          .WithDescription("Gets all products from the postgres database.");
    }
}

