using DDD.Core.Contracts.Library.ApplicationServices.Queries;
using DDD.Core.RequestResponse.Library.Queries;

namespace DDD.Core.ApplicationServices.Library.Queries;

/// <summary>
/// 
/// </summary>
public abstract class QueryDispatcherDecorator : IQueryDispatcher
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    protected IQueryDispatcher _queryDispatcher;
    /// <summary>
    /// 
    /// </summary>
    public abstract int Order { get; }
    #endregion

    #region Constructors
    public QueryDispatcherDecorator() { }
    #endregion

    #region Query Dispatcher
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public abstract Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query) where TQuery : class, IQuery<TData>;
    #endregion

    #region Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryDispatcher"></param>
    public void SetQueryDispatcher(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }
    #endregion
}