using BaseSource.Core.Application.UseCases.Workflow.Tasks.Models.DTOs;
using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Repositories;

namespace BaseSource.Core.Application.UseCases.Workflow.Tasks.Queries.GetTaskById;

public class GetTaskByIdQueryValidator : AbstractValidator<GetTaskByIdQuery>
{
    public GetTaskByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Task ID is required.");
    }
}

public record GetTaskByIdQuery(Guid Id) : IQuery<Result<TaskDto>>;

public class GetTaskByIdQueryHandler : QueryHandler<GetTaskByIdQuery, Result<TaskDto>>
{
    private readonly ITaskRepository _taskRepository;
    public GetTaskByIdQueryHandler(ProviderFactory factory, ITaskRepository taskRepository) : base(factory)
    {
        _taskRepository = taskRepository;
    }

    public override async Task<Result<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

        if (task == null)
            return (Result<TaskDto>)Result<TaskDto>.Failure("Task not found");

        var dto = new TaskDto
        {
            EntityId = task.EntityId.ToValue(),
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            Status = task.Status,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            CompletedAt = task.CompletedAt
        };

        return Result<TaskDto>.Success(dto);
    }
}



