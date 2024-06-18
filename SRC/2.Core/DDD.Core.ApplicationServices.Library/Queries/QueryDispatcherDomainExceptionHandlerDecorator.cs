using DDD.Core.Domain.Library.Exceptions;
using DDD.Core.RequestResponse.Library.Common;
using DDD.Core.RequestResponse.Library.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Extensions.Logger.Abstractions;
using Extensions.Translations.Abstractions;

namespace DDD.Core.ApplicationServices.Library.Queries;

/// <summary>
/// 
/// </summary>
public class QueryDispatcherDomainExceptionHandlerDecorator : QueryDispatcherDecorator
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    private readonly IServiceProvider _serviceProvider;
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<QueryDispatcherDomainExceptionHandlerDecorator> _logger;
    /// <summary>
    /// 
    /// </summary>
    public override int Order => 2;
    #endregion

    #region Constructors
    public QueryDispatcherDomainExceptionHandlerDecorator(IServiceProvider serviceProvider,
                                                          ILogger<QueryDispatcherDomainExceptionHandlerDecorator> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    #endregion

    #region Query Dispatcher
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public override async Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query)
    {
        try
        {
            var result = await _queryDispatcher.Execute<TQuery, TData>(query);
            return result;

        }
        catch (DomainStateException ex)
        {
            _logger.LogError(LoggingEventId.DomainValidationException, ex, "Processing of {QueryType} With value {Query} failed at {StartDateTime} because there are domain exceptions.", query.GetType(), query, DateTime.Now);
            return DomainExceptionHandlingWithReturnValue<TQuery, TData>(ex);
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is DomainStateException domainStateException)
            {
                _logger.LogError(LoggingEventId.DomainValidationException, ex, "Processing of {QueryType} With value {Query} failed at {StartDateTime} because there are domain exceptions.", query.GetType(), query, DateTime.Now);
                return DomainExceptionHandlingWithReturnValue<TQuery, TData>(domainStateException);
            }
            throw ex;
        }
    }
    #endregion

    #region Privaite Methods
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="ex"></param>
    /// <returns></returns>
    private QueryResult<TData> DomainExceptionHandlingWithReturnValue<TQuery, TData>(DomainStateException ex)
    {
        var queryResult = new QueryResult<TData>()
        {
            Status = ApplicationServiceStatus.InvalidDomainState
        };

        queryResult.AddMessage(GetExceptionText(ex));

        return queryResult;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="domainStateException"></param>
    /// <returns></returns>
    private string GetExceptionText(DomainStateException domainStateException)
    {
        var translator = _serviceProvider.GetService<ITranslator>();
        if (translator == null)
            return domainStateException.ToString();

        var result = domainStateException?.Parameters.Any() == true ?
             translator[domainStateException.Message, domainStateException.Parameters] :
               translator[domainStateException?.Message];

        _logger.LogInformation(LoggingEventId.DomainValidationException, "Domain Exception message is {DomainExceptionMessage}", result);

        return result;
    }
    #endregion
}