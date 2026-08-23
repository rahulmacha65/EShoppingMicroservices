
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
public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBusketCommand, DeleteBusketResult>
{
    public async Task<DeleteBusketResult> Handle(DeleteBusketCommand command, CancellationToken cancellationToken)
    {
        // TODO - Delete basket by usernName
        await repository.DeleteBasket(command.UserName, cancellationToken);

        return new DeleteBusketResult(true);
    }
}
