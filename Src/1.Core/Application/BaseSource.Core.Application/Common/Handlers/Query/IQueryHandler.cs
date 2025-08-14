using MediatR;

namespace BaseSource.Core.Application.Common.Handlers.Query;

public interface IQuery<TResponse> : IRequest<TResponse> { }
public abstract class Query<TResponse> : IQuery<TResponse> { }

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : Query<TResponse>
{
}
public abstract class QueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
    where TQuery : Query<TResponse>
{
    public abstract Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);

}
