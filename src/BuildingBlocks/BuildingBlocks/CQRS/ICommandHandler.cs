using MediatR;

namespace BuildingBlocks.CQRS;

//Use this interface to handle commands that do not return a response.
public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand
{
}

//Use this interface to handle commands that return a response.
public interface ICommandHandler<in TCommand, IResponse> : IRequestHandler<TCommand, IResponse>
    where TCommand : ICommand<IResponse>
    where IResponse : notnull
{
}

