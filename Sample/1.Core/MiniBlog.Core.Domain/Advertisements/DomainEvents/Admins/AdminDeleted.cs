using DDD.Core.Domain.Library.Events;

namespace MiniBlog.Core.Domain.Advertisements.DomainEvents.Courses;

public sealed record AdminDeleted(Guid BusinessId, long Id) :IDomainEvent;
