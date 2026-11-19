namespace Ordering.API.EndPoints;

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{orderId}", async (Guid orderId, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(orderId));

            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);

        }).WithName("Deletes Order")
          .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Deletes order")
          .WithDescription("Deletes order in the system.");
    }
}
