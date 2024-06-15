
using DDD.Core.RequestResponse.Library.Commands;

namespace DDD.Core.Contracts.Library.ApplicationServices.Commands;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TData"></typeparam>
public interface ICommandHandler<TCommand, TData> where TCommand : ICommand<TData>
{
    Task<CommandResult<TData>> Handle(TCommand request);
}
/// <summary>
/// 
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<CommandResult> Handle(TCommand request);
}

