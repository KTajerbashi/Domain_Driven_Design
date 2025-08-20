namespace BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Events;

public record TaskCreatedEvent(EntityId TaskId, string Title) : DomainEvent;
public record TaskUpdatedEvent(EntityId TaskId) : DomainEvent;
public record TaskCompletedEvent(EntityId TaskId) : DomainEvent;
public record TaskReopenedEvent(EntityId TaskId) : DomainEvent;