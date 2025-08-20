using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Entities;

namespace BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(TaskItem task, CancellationToken cancellationToken = default);
    void Update(TaskItem task);
    void Delete(TaskItem task);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}