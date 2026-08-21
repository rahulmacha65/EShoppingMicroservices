namespace Basket.API.Basket.GetBasket;

public record GetBaskQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);
public class GetBasketHandler : IQueryHandler<GetBaskQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBaskQuery query, CancellationToken cancellationToken)
    {
        //TODO: get basket from database
        // var basket = await _repository.GetBasket(query.UserName);

        return new GetBasketResult(new ShoppingCart("rahul"));
    }
}
