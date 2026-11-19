namespace Ordering.API.EndPoints;

public record GetOrdersResponse(PaginationResult<OrderDto> Orders);
public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var query = new GetOrdersQuery(request);

            var result = await sender.Send(query);

            var response = result.Adapt<GetOrdersResponse>();

            return Results.Ok(response);

        }).WithName("Get Pagintaed Orders")
          .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get paginated orders")
          .WithDescription("Get paginated orders from the system.");
    }
}
