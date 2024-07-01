using DDD.Core.Domain.Library.Events;

namespace MiniBlog.Core.Domain.Advertisements.DomainEvents.Courses;

public sealed record CourseDeleted(Guid BusinessId, long Id, List<AdminDeleted> Admins) : IDomainEvent;
