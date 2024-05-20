
using DDD.Core.RequestResponse.Library.Commands;

namespace DDD.Core.Contracts.Library.ApplicationServices.Commands;

public interface ICommandHandler<TCommand, TData> where TCommand : ICommand<TData>
{
    Task<CommandResult<TData>> Handle(TCommand request);
}
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<CommandResult> Handle(TCommand request);
}

