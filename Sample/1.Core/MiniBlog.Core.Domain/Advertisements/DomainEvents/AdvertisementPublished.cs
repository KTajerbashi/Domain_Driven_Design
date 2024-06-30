using DDD.Core.Domain.Library.Events;

namespace MiniBlog.Core.Domain.Advertisements.DomainEvents;

public sealed record AdvertisementPublished(Guid BusinessId, long Id, DateTime PublishDate) : IDomainEvent;
