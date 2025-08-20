using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Events;

namespace BaseSource.Core.Application.UseCases.Workflow.Tasks.Events;


public class TaskCreatedEventHandler : INotificationHandler<TaskCreatedEvent>
{
    private readonly ILogger<TaskCreatedEventHandler> _logger;

    public TaskCreatedEventHandler(ILogger<TaskCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TaskCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Task created: {TaskId} - {Title}", notification.TaskId, notification.Title);

        // Here you could:
        // - Send notifications
        // - Update search indexes
        // - Trigger workflows
        // - etc.

        return Task.CompletedTask;
    }
}


public class TaskCompletedEventHandler : INotificationHandler<TaskCompletedEvent>
{
    private readonly ILogger<TaskCompletedEventHandler> _logger;

    public TaskCompletedEventHandler(ILogger<TaskCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TaskCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Task completed: {TaskId}", notification.TaskId);

        // Handle task completion logic
        return Task.CompletedTask;
    }
}