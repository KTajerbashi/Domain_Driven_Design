using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Enums;
using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Repositories;

namespace BaseSource.Core.Application.UseCases.Workflow.Tasks.Commands.Update;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Task ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.");
    }
}
public record UpdateTaskCommand(
    Guid Id,
    string Title,
    string Description,
    TaskPriorityEnum Priority,
    DateTime DueDate) : IRequest<Result>;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result>
{
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

        if (task == null)
            return Result.Failure("Task not found");

        try
        {
            task.Update(request.Title, request.Description, request.Priority, request.DueDate);
            _taskRepository.Update(task);

            return Result.Success();
        }
        catch (Domain.Exceptions.DomainException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}