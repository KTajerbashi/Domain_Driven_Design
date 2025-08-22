namespace BaseSource.Core.Domain.Aggregates.Store.Orders.Enums;

public enum OrderStatusEnum : byte
{
    Pending,
    Confirmed,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}
