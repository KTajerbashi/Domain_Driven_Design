using BaseSource.Core.Application.Common.Handlers.Query;
using BaseSource.Core.Application.UseCases.Workflow.Tasks.Models.DTOs;
using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Repositories;

namespace BaseSource.Core.Application.UseCases.Workflow.Tasks.Queries.GetAllTasks;

public record GetAllTasksQuery : IQuery<Result<List<TaskDto>>>;

public class GetAllTasksQueryHandler : QueryHandler<GetAllTasksQuery, Result<List<TaskDto>>>
{
    private readonly ITaskRepository _taskRepository;

    public GetAllTasksQueryHandler(ProviderFactory factory, ITaskRepository taskRepository) : base(factory)
    {
        _taskRepository = taskRepository;
    }

    public override async Task<Result<List<TaskDto>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync(cancellationToken);

        var dtos = tasks.Select(task => new TaskDto
        {
            EntityId = task.EntityId.ToValue(),
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            Status = task.Status,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            CompletedAt = task.CompletedAt
        }).ToList();

        return Result<List<TaskDto>>.Success(dtos);
    }
}
