using DDD.Core.Contracts.Library.ApplicationServices.Commands;
using DDD.Core.RequestResponse.Library.Commands;
using Extensions.Logger.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DDD.Core.ApplicationServices.Library.Commands;
/// <summary>
/// 
/// </summary>
public class CommandDispatcher : ICommandDispatcher
{
    #region Fields

    /// <summary>
    /// 
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<CommandDispatcher> _logger;

    /// <summary>
    /// 
    /// </summary>
    private readonly Stopwatch _stopwatch;
    #endregion

    #region Constructors
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="logger"></param>
    public CommandDispatcher(IServiceProvider serviceProvider, ILogger<CommandDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _stopwatch = new Stopwatch();
        _logger = logger;
    }
    #endregion

    #region Send Commands
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        _stopwatch.Start();
        try
        {
            _logger.LogDebug("Routing command of type {CommandType} With value {Command}  Start at {StartDateTime}", command.GetType(), command, DateTime.Now);
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

            return await handler.Handle(command);

        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "There is not suitable handler for {CommandType} Routing failed at {StartDateTime}.", command.GetType(), DateTime.Now);
            throw;
        }
        finally
        {
            _stopwatch.Stop();
            _logger.LogInformation(LoggingEventId.PerformanceMeasurement, "Processing the {CommandType} command tooks {Millisecconds} Millisecconds", command.GetType(), _stopwatch.ElapsedMilliseconds);
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command) where TCommand : class, ICommand<TData>
    {
        _stopwatch.Start();
        try
        {
            _logger.LogDebug("Routing command of type {CommandType} With value {Command}  Start at {StartDateTime}", command.GetType(), command, DateTime.Now);
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TData>>();
            return await handler.Handle(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There is not suitable handler for {CommandType} Routing failed at {StartDateTime}.", command.GetType(), DateTime.Now);
            throw;
        }
        finally
        {
            _stopwatch.Stop();
            _logger.LogInformation("Processing the {CommandType} command tooks {Millisecconds} Millisecconds", command.GetType(), _stopwatch.ElapsedMilliseconds);
        }
    }

    #endregion
}