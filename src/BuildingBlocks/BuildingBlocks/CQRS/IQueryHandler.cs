using MediatR;

namespace BuildingBlocks.CQRS;


public interface IQueryHandler<in TQuery, IResponse> : IRequestHandler<TQuery, IResponse>
    where TQuery : IQuery<IResponse>
    where IResponse : notnull
{
}
