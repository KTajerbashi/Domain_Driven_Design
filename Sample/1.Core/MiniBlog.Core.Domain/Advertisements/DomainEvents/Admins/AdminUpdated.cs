using DDD.Core.Domain.Library.Events;

namespace MiniBlog.Core.Domain.Advertisements.DomainEvents.Courses;

public sealed record AdminUpdated(Guid BusinessId,string Title, int RoleId) :IDomainEvent;
