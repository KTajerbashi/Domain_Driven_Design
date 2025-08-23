
namespace BaseSource.Core.Application.Common.Handlers.Query;

public interface IQuery<TResponse> : IRequest<TResponse> { }
//public abstract class Query<TResponse> : IQuery<TResponse> { }

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}
public abstract class QueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    protected readonly ProviderFactory Factory;
    protected QueryHandler(ProviderFactory factory)
    {
        Factory = factory;
    }
    public abstract Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);

}
