using MediatR;

namespace BuildingBlocks.CQRS;

//Use this interface to represent a command that does not return a response.
public interface ICommand : ICommand<Unit>
{
}

//Use this interface to represent a command that returns a response.
public interface ICommand<out IResponse> : IRequest<IResponse>
{
}

