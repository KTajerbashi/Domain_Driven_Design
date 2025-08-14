using MediatR;

namespace BaseSource.Core.Application.Common.Handlers.Command;

public interface ICommand : IRequest { }
public abstract class Command : ICommand { }
public interface ICommand<TResponse> : IRequest<TResponse> { }
public abstract class Command<TResponse> : ICommand<TResponse> { }

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : Command

{
}

public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : Command
{
    public abstract Task Handle(TCommand command, CancellationToken cancellationToken);

}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : Command<TResponse>
{
}
public abstract class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
    where TCommand : Command<TResponse>
{
    public abstract Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);

}