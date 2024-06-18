using DDD.Core.RequestResponse.Library.Common;
using DDD.Core.RequestResponse.Library.Queries;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Extensions.Logger.Abstractions;

namespace DDD.Core.ApplicationServices.Library.Queries;
/// <summary>
/// 
/// </summary>
public class QueryDispatcherValidationDecorator : QueryDispatcherDecorator
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    private readonly IServiceProvider _serviceProvider;
    
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<QueryDispatcherValidationDecorator> _logger;
    
    /// <summary>
    /// 
    /// </summary>
    public override int Order => 1;
    #endregion

    #region Constructors
    public QueryDispatcherValidationDecorator(IServiceProvider serviceProvider,
                                              ILogger<QueryDispatcherValidationDecorator> logger)
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
        _logger.LogDebug(LoggingEventId.QueryValidation, "Validating query of type {QueryType} With value {Query}  start at :{StartDateTime}", query.GetType(), query, DateTime.Now);

        var validationResult = Validate<TQuery, QueryResult<TData>>(query);

        if (validationResult != null)
        {
            _logger.LogInformation(LoggingEventId.QueryValidation, "Validating query of type {QueryType} With value {Query}  failed. Validation errors are: {ValidationErrors}", query.GetType(), query, validationResult.Messages);
            return validationResult;
        }

        _logger.LogDebug(LoggingEventId.QueryValidation, "Validating query of type {QueryType} With value {Query}  finished at :{EndDateTime}", query.GetType(), query, DateTime.Now);
        return await _queryDispatcher.Execute<TQuery, TData>(query);
    }
    #endregion

    #region Privaite Methods
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValidationResult"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    private TValidationResult Validate<TQuery, TValidationResult>(TQuery query) where TValidationResult : ApplicationServiceResult, new()
    {
        var validator = _serviceProvider.GetService<IValidator<TQuery>>();
        TValidationResult res = null;

        if (validator != null)
        {
            var validationResult = validator.Validate(query);
            if (!validationResult.IsValid)
            {
                res = new()
                {
                    Status = ApplicationServiceStatus.ValidationError
                };
                foreach (var item in validationResult.Errors)
                {
                    res.AddMessage(item.ErrorMessage);
                }
            }
        }
        else
        {
            _logger.LogInformation(LoggingEventId.CommandValidation, "There is not any validator for {QueryType}", query.GetType());
        }
        return res;
    }
    #endregion
}