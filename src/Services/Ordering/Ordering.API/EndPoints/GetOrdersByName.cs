namespace Ordering.API.EndPoints;

public record GetOrderByNameResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
        {
            var query = new GetOrderByNameQuery(orderName);

            var orders = await sender.Send(query);

            var response = orders.Adapt<GetOrderByNameResponse>();

            return Results.Ok(response);

        }).WithName("Get Order By Name")
          .Produces<GetOrderByNameResponse>(StatusCodes.Status200OK)
          .Produces<GetOrderByNameResponse>(StatusCodes.Status404NotFound)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get orders")
          .WithDescription("Get orders from the system.");
    }
}
