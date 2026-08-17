
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryRequest(IEnumerable<Product> product);
public class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByCategoryQuery(category));

            var response = result.Adapt<GetProductByCategoryRequest>();

            return Results.Ok(response);
        }).WithName("GetProductByCategory")
          .Produces<GetProductByCategoryRequest>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound)
          .WithSummary("Get products by category")
          .WithDescription("Retrieves a list of products that belong to the specified category.");
    }
}

