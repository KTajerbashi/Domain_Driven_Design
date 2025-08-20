using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Enums;
using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Events;

namespace BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Entities;

public class TaskItem : AggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskPriorityEnum Priority { get; private set; }
    public TaskStatusEnum Status { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    // Domain events
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private TaskItem() { } // For EF Core

    public TaskItem(string title, string description, TaskPriorityEnum priority, DateTime dueDate)
    {
        Title = title;
        Description = description;
        Priority = priority;
        Status = TaskStatusEnum.Pending;
        DueDate = dueDate;
        CreatedAt = DateTime.UtcNow;

        ValidateState();

        // Raise domain event
        AddDomainEvent(new TaskCreatedEvent(EntityId, Title));
    }

    public void Update(string title, string description, TaskPriorityEnum priority, DateTime dueDate)
    {
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;

        ValidateState();

        AddDomainEvent(new TaskUpdatedEvent(EntityId));
    }

    public void Complete()
    {
        if (Status == TaskStatusEnum.Completed)
            throw new DomainException("Task is already completed");

        Status = TaskStatusEnum.Completed;
        CompletedAt = DateTime.UtcNow;

        AddDomainEvent(new TaskCompletedEvent(EntityId));
    }

    public void Reopen()
    {
        if (Status != TaskStatusEnum.Completed)
            throw new DomainException("Only completed tasks can be reopened");

        Status = TaskStatusEnum.Pending;
        CompletedAt = null;

        AddDomainEvent(new TaskReopenedEvent(EntityId));
    }

    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    private void ValidateState()
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new DomainException("Title is required");

        if (Title.Length > 200)
            throw new DomainException("Title cannot exceed 200 characters");

        if (Description?.Length > 1000)
            throw new DomainException("Description cannot exceed 1000 characters");

        if (DueDate < DateTime.UtcNow.Date)
            throw new DomainException("Due date cannot be in the past");
    }

    public static TaskItem? GetInstance()
    {
        return new TaskItem();
    }
}