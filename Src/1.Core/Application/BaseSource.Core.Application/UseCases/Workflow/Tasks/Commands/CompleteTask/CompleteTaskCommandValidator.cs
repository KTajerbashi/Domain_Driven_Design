using BaseSource.Core.Application.Common.Handlers.Command;
using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Repositories;

namespace BaseSource.Core.Application.UseCases.Workflow.Tasks.Commands.CompleteTask;

public class CompleteTaskCommandValidator : AbstractValidator<CompleteTaskCommand>
{
    public CompleteTaskCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Task ID is required.");
    }
}

public record CompleteTaskCommand(Guid Id) : ICommand<Result>;

public class CompleteTaskCommandHandler : CommandHandler<CompleteTaskCommand, Result>
{
    private readonly ITaskRepository _taskRepository;

    public CompleteTaskCommandHandler(ProviderFactory factory, ITaskRepository taskRepository) : base(factory)
    {
        _taskRepository = taskRepository;
    }

    public override async Task<Result> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

        if (task == null)
            return Result.Failure("Task not found");

        try
        {
            task.Complete();
            _taskRepository.Update(task);

            return Result.Success();
        }
        catch (Domain.Exceptions.DomainException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}