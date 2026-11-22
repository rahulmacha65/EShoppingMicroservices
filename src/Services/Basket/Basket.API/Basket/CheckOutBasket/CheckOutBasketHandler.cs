using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckOutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckOutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckOutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can not be null");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotNull().WithMessage("UserName is required");
    }
}
public class CheckOutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndPoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        //Get existing basket with total price
        var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        if (basket == null)
        {
            return new CheckoutBasketResult(false);
        }

        //set totalprice on basketcheckout event message
        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckOutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        //send basket checkout event to message broker
        await publishEndPoint.Publish(eventMessage, cancellationToken);

        //delete the basket
        await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}
