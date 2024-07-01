using DDD.Core.Domain.Library.Events;

namespace MiniBlog.Core.Domain.Advertisements.DomainEvents.Courses;

public sealed record CourseUpdated(Guid BusinessId, string Name, int Length, DateTime From, DateTime To, List<AdminUpdated> Admins) : IDomainEvent;
