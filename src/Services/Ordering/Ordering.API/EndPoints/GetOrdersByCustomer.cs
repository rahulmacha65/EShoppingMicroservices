namespace Ordering.API.EndPoints;

public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var query = new GetOrderByCustomerQuery(customerId);

            var result = await sender.Send(query);

            var response = result.Adapt<GetOrderByCustomerResponse>();

            return Results.Ok(response);

        }).WithName("Get Order By Customer")
          .Produces<GetOrderByCustomerResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get orders by customer")
          .WithDescription("Get orders from the system by customer Id.");
    }
}
