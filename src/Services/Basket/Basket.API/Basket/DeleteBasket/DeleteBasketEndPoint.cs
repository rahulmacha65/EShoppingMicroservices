namespace Basket.API.Basket.DeleteBasket;

//public record DeleteBusketRequest(string UserName);
public record DeleteBusketResponse(bool IsSuccess);
public class DeleteBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBusketCommand(username));
            var response = result.Adapt<DeleteBusketResponse>();
            return Results.Ok(response);
        }).WithName("Delete busket")
        .Produces<DeleteBusketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete busket by username")
        .WithDescription("Delete busket by using the username");
    }
}
