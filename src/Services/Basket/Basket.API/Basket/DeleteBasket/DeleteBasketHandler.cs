
namespace Basket.API.Basket.DeleteBasket;

public record DeleteBusketCommand(string UserName) : ICommand<DeleteBusketResult>;
public record DeleteBusketResult(bool IsSuccess);

public class DeleteBusketCommandValidator : AbstractValidator<DeleteBusketCommand>
{
    public DeleteBusketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
    }
}
public class DeleteBasketHandler : ICommandHandler<DeleteBusketCommand, DeleteBusketResult>
{
    public async Task<DeleteBusketResult> Handle(DeleteBusketCommand command, CancellationToken cancellationToken)
    {
        // TODO - find the basket by usernName and delete

        //session.Delete<Product>(command.Id);
        return new DeleteBusketResult(true);
    }
}
