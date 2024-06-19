using DDD.Core.Domain.Library.Exceptions;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Core.RequestResponse.Library.Common;
using Extensions.Logger.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Extensions.Translations.Abstractions;

namespace DDD.Core.ApplicationServices.Library.Commands;

/// <summary>
/// 
/// </summary>
public class CommandDispatcherDomainExceptionHandlerDecorator : CommandDispatcherDecorator
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    private readonly IServiceProvider _serviceProvider;
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<CommandDispatcherDomainExceptionHandlerDecorator> _logger;
    #endregion

    #region Constructors
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="logger"></param>
    public CommandDispatcherDomainExceptionHandlerDecorator(
        IServiceProvider serviceProvider,
        ILogger<CommandDispatcherDomainExceptionHandlerDecorator> logger
        )
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    /// <summary>
    /// 
    /// </summary>
    public override int Order => 2;
    #endregion

    #region Send Commands
   
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <param name="command"></param>
    /// <returns></returns>
    public override async Task<CommandResult> Send<TCommand>(TCommand command)
    {
        try
        {
            var result = _commandDispatcher.Send(command);
            return await result;
        }
        catch (DomainStateException ex)
        {
            _logger.LogError(LoggingEventId.DomainValidationException, ex, "Processing of {CommandType} With value {Command} failed at {StartDateTime} because there are domain exceptions.", command.GetType(), command, DateTime.Now);
            return DomainExceptionHandlingWithoutReturnValue<TCommand>(ex);
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is DomainStateException domainStateException)
            {
                _logger.LogError(LoggingEventId.DomainValidationException, domainStateException, "Processing of {CommandType} With value {Command} failed at {StartDateTime} because there are domain exceptions.", command.GetType(), command, DateTime.Now);
                return DomainExceptionHandlingWithoutReturnValue<TCommand>(domainStateException);
            }
            throw ex;
        }

    }
   
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="command"></param>
    /// <returns></returns>
    public override async Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command)
    {
        try
        {
            var result = await _commandDispatcher.Send<TCommand, TData>(command);
            return result;

        }
        catch (DomainStateException ex)
        {
            _logger.LogError(LoggingEventId.DomainValidationException, ex, "Processing of {CommandType} With value {Command} failed at {StartDateTime} because there are domain exceptions.", command.GetType(), command, DateTime.Now);
            return DomainExceptionHandlingWithReturnValue<TCommand, TData>(ex);
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is DomainStateException domainStateException)
            {
                _logger.LogError(LoggingEventId.DomainValidationException, ex, "Processing of {CommandType} With value {Command} failed at {StartDateTime} because there are domain exceptions.", command.GetType(), command, DateTime.Now);
                return DomainExceptionHandlingWithReturnValue<TCommand, TData>(domainStateException);
            }
            throw ex;
        }
    }
    #endregion

    #region Privaite Methods
   
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <param name="ex"></param>
    /// <returns></returns>
    private CommandResult DomainExceptionHandlingWithoutReturnValue<TCommand>(DomainStateException ex)
    {
        var commandResult = new CommandResult
        {
            Status = ApplicationServiceStatus.InvalidDomainState
        };

        commandResult.AddMessage(GetExceptionText(ex));

        return commandResult;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="ex"></param>
    /// <returns></returns>
    private CommandResult<TData> DomainExceptionHandlingWithReturnValue<TCommand, TData>(DomainStateException ex)
    {
        var commandResult = new CommandResult<TData>()
        {
            Status = ApplicationServiceStatus.InvalidDomainState
        };

        commandResult.AddMessage(GetExceptionText(ex));

        return commandResult;
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

