namespace Catalog.API.Products.CreateProduct;

public record class CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record class CreateProductResponse(Guid Id);

public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            // Map the request to the command using Mapster
            var command = request.Adapt<CreateProductCommand>();

            // Send the command to the MediatR pipeline
            var result = await sender.Send(command);

            // Map the result to the response using Mapster
            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        }).WithName("CreateProduct")
          .Produces<CreateProductResponse>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Creates a new product")
          .WithDescription("Creates a new product with the specified details.");

    }
}

