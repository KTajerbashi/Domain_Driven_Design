
using DDD.Core.Contracts.Library.ApplicationServices.Commands;
using DDD.Core.RequestResponse.Library.Commands;

namespace DDD.Core.ApplicationServices.Library.Commands;

/// <summary>
/// 
/// </summary>
public abstract class CommandDispatcherDecorator : ICommandDispatcher
{
    #region Fields
    protected ICommandDispatcher _commandDispatcher;
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
    public abstract Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand;

    public abstract Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command) where TCommand : class, ICommand<TData>;
    #endregion
}

