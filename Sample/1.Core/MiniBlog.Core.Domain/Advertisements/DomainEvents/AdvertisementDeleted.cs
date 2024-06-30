using DDD.Core.Domain.Library.Events;

namespace MiniBlog.Core.Domain.Advertisements.DomainEvents;

public sealed record AdvertisementDeleted(Guid BusinessId, long Id) : IDomainEvent;
