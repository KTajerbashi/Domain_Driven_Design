using DDD.Core.RequestResponse.Library.Commands;
using DDD.Core.RequestResponse.Library.Common;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Extensions.Logger.Abstractions;

namespace DDD.Core.ApplicationServices.Library.Commands;

/// <summary>
/// این اولین مرحله دیسپچر است
/// </summary>
public class CommandDispatcherValidationDecorator : CommandDispatcherDecorator
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    private readonly IServiceProvider _serviceProvider;
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<CommandDispatcherValidationDecorator> _logger;
    #endregion

    #region Constructors
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="logger"></param>
    public CommandDispatcherValidationDecorator(IServiceProvider serviceProvider,
                                                ILogger<CommandDispatcherValidationDecorator> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    /// <summary>
    /// این تعیین کننده مراحل اجرا است
    /// </summary>
    public override int Order => 1;
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
        _logger.LogDebug(EventID.CommandValidation, "Validating command of type {CommandType} With value {Command}  start at :{StartDateTime}", command.GetType(), command, DateTime.Now);
        var validationResult = Validate<TCommand, CommandResult>(command);

        if (validationResult != null)
        {
            _logger.LogInformation(EventID.CommandValidation, "Validating command of type {CommandType} With value {Command}  failed. Validation errors are: {ValidationErrors}", command.GetType(), command, validationResult.Messages);
            return validationResult;
        }
        _logger.LogDebug(EventID.CommandValidation, "Validating command of type {CommandType} With value {Command}  finished at :{EndDateTime}", command.GetType(), command, DateTime.Now);
        return await _commandDispatcher.Send(command);
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
        _logger.LogDebug(EventID.CommandValidation, "Validating command of type {CommandType} With value {Command}  start at :{StartDateTime}", command.GetType(), command, DateTime.Now);

        var validationResult = Validate<TCommand, CommandResult<TData>>(command);

        if (validationResult != null)
        {
            _logger.LogInformation(EventID.CommandValidation, "Validating command of type {CommandType} With value {Command}  failed. Validation errors are: {ValidationErrors}", command.GetType(), command, validationResult.Messages);
            return validationResult;
        }
        _logger.LogDebug(EventID.CommandValidation, "Validating command of type {CommandType} With value {Command}  finished at :{EndDateTime}", command.GetType(), command, DateTime.Now);
        return await _commandDispatcher.Send<TCommand, TData>(command);
    }
    #endregion

    #region Privaite Methods
    /// <summary>
    /// این متد بصورت توکار تمامی ولیدیشن های این دستور را دریافت و اجرا  میکند رو دستور مورد نظر
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TValidationResult"></typeparam>
    /// <param name="command"></param>
    /// <returns></returns>
    private TValidationResult Validate<TCommand, TValidationResult>(TCommand command) where TValidationResult : ApplicationServiceResult, new()
    {
        var validator = _serviceProvider.GetService<IValidator<TCommand>>();
        TValidationResult res = null;

        if (validator != null)
        {
            var validationResult = validator.Validate(command);
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
            _logger.LogInformation(EventID.CommandValidation, "There is not any validator for {CommandType}", command.GetType());
        }
        return res;
    }
    #endregion
}
