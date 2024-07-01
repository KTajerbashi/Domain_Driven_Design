using DDD.Core.Domain.Library.Events;

namespace MiniBlog.Core.Domain.Advertisements.DomainEvents.Courses;

public sealed record CourseCreated(Guid BusinessId, string Name, int Length, DateTime From, DateTime To, List<AdminCreated> Admins) : IDomainEvent;
