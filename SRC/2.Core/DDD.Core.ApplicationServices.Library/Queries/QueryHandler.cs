using DDD.Core.Contracts.Library.ApplicationServices.Queries;
using DDD.Core.RequestResponse.Library.Common;
using DDD.Core.RequestResponse.Library.Queries;
using DDD.Utilities.Library;

namespace DDD.Core.ApplicationServices.Library.Queries;
public abstract class QueryHandler<TQuery, TData> : IQueryHandler<TQuery, TData>
    where TQuery : class, IQuery<TData>
{
    protected readonly UtilitiesServices UtilitiesServices;
    protected readonly QueryResult<TData> result = new();

    protected virtual Task<QueryResult<TData>> ResultAsync(TData data, ApplicationServiceStatus status)
    {
        result._data = data;
        result.Status = status;
        return Task.FromResult(result);
    }

    protected virtual QueryResult<TData> Result(TData data, ApplicationServiceStatus status)
    {
        result._data = data;
        result.Status = status;
        return result;
    }

    protected virtual Task<QueryResult<TData>> ResultAsync(TData data)
    {
        var status = data != null ? ApplicationServiceStatus.Ok : ApplicationServiceStatus.NotFound;
        return ResultAsync(data, status);
    }

    protected virtual QueryResult<TData> Result(TData data)
    {
        var status = data != null ? ApplicationServiceStatus.Ok : ApplicationServiceStatus.NotFound;
        return Result(data, status);
    }

    public QueryHandler(UtilitiesServices utilitiesServices)
    {
        UtilitiesServices = utilitiesServices;
    }

    protected void AddMessage(string message)
    {
        result.AddMessage(UtilitiesServices.Translator[message]);
    }

    protected void AddMessage(string message, params string[] arguments)
    {
        result.AddMessage(UtilitiesServices.Translator[message, arguments]);
    }

    public abstract Task<QueryResult<TData>> Handle(TQuery query);
}
