namespace BaseSource.Core.Domain.Aggregates.Store.Products.Events;

public record ProductCreatedEvent(EntityId EntityId) : DomainEvent;
public record ProductUpdatedEvent(EntityId EntityId) : DomainEvent;
public record ProductStockUpdatedEvent(EntityId EntityId) : DomainEvent;
public record ProductDetailAddedEvent(EntityId EntityId) : DomainEvent;
public record ProductDetailDeletedEvent(EntityId EntityId) : DomainEvent;
public record ProductImageAddedEvent(EntityId EntityId) : DomainEvent;
public record ProductCommentAddedEvent(EntityId EntityId) : DomainEvent;
public record ProductCommentRemovedEvent(EntityId EntityId) : DomainEvent;
