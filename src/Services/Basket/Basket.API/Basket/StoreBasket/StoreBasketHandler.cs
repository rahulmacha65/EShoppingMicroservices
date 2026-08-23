namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Car can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required");
    }
}
public class StoreBasketHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        //TODO - store basket in database.
        var result = await repository.StoreBasket(cart, cancellationToken);

        //TODO - Update cache.

        return new StoreBasketResult(result.UserName);
    }
}

