using DDD.Core.Contracts.Library.ApplicationServices.Commands;
using DDD.Core.RequestResponse.Library.Commands;

namespace DDD.Core.ApplicationServices.Library.Commands;

/// <summary>
/// Chain Of Responsibility
/// </summary>
public abstract class CommandDispatcherDecorator : ICommandDispatcher
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    protected ICommandDispatcher _commandDispatcher;

    /// <summary>
    /// 
    /// </summary>
    public abstract int Order { get; }
    #endregion

    #region Constructors
    public CommandDispatcherDecorator()
    {
    }
    #endregion

    /// <summary>
    /// این متد به ما امکان ست کردن فیلد کاماند دیسپچر را میدهد
    /// </summary>
    /// <param name="commandDispatcher"></param>
    public void SetCommandDispatcher(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }
  
    #region Abstract Send Commands
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <param name="command"></param>
    /// <returns></returns>
    public abstract Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="command"></param>
    /// <returns></returns>
    public abstract Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command) where TCommand : class, ICommand<TData>;
    #endregion
}

