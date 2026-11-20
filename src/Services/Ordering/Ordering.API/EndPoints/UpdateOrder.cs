namespace Ordering.API.EndPoints;

public record UpdateOrderRequest(OrderDto orderDto);
public record UpdateOrderResponse(bool Issuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateOrderCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateOrderResponse>();

            return Results.Ok(response);

        }).WithName("Updates Order")
          .Produces<UpdateOrderResponse>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Updates order")
          .WithDescription("Updates order in the system.");
    }
}
