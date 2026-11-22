
using Basket.API.Dtos;

namespace Basket.API.Basket.CheckOutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckOutBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckoutBasketRequest BasketCheckoutDto, ISender sender) =>
        {
            var command = BasketCheckoutDto.Adapt<CheckoutBasketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CheckoutBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("CheckOutBasket")
        .Produces<CheckoutBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Check out basket")
        .WithSummary("Check out basket for purchase");
    }
}
