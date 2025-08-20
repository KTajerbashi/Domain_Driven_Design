using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Entities;
using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Repositories;

namespace BaseSource.Infrastructure.SQL.Command.Repositories.Workflow;
public class TaskRepository : ITaskRepository, IScopedLifetime
{


    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return TaskItem.GetInstance();
        //return await _context.Tasks
        //    .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return Enumerable.Range(1,10).Select(item => TaskItem.GetInstance()).ToList()!;
        //return await _context.Tasks
        //    .OrderByDescending(t => t.CreatedAt)
        //    .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        //await _context.Tasks.AddAsync(task, cancellationToken);
    }

    public void Update(TaskItem task)
    {
        //_context.Tasks.Update(task);
    }

    public void Delete(TaskItem task)
    {
        //_context.Tasks.Remove(task);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return true;
        //return await _context.Tasks
        //    .AnyAsync(t => t.Id == id, cancellationToken);
    }
}