using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Entities;
using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Enums;
using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Repositories;

namespace BaseSource.Core.Application.UseCases.Workflow.Tasks.Commands.Create;
public record CreateTaskCommand(
    string Title,
    string Description,
    TaskPriorityEnum Priority,
    DateTime DueDate) : ICommand<Result<Guid>>;
public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Priority must be a valid value.");

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.");
    }
}

public class CreateTaskCommandHandler : CommandHandler<CreateTaskCommand, Result<Guid>>
{
    private readonly ITaskRepository _taskRepository;

    public CreateTaskCommandHandler(ProviderFactory factory, ITaskRepository taskRepository) : base(factory)
    {
        _taskRepository = taskRepository;
    }

    public override async Task<Result<Guid>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var task = new TaskItem(
                request.Title,
                request.Description,
                request.Priority,
                request.DueDate);

            await _taskRepository.AddAsync(task, cancellationToken);

            return Result.Success<Guid>(task.EntityId.ToValue());
        }
        catch (Domain.Exceptions.DomainException ex)
        {
            return Result.Failure<Guid>(ex.Message);
        }
    }
}
