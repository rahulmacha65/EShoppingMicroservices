namespace Basket.API.Basket.GetBasket;

public record GetBaskQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);
public class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBaskQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBaskQuery query, CancellationToken cancellationToken)
    {
        var result = await repository.GetBasket(query.UserName, cancellationToken);
        return new GetBasketResult(result);
    }
}
