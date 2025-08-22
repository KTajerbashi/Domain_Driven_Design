using BaseSource.Core.Domain.Aggregates.Store.Orders.Enums;

namespace BaseSource.Core.Domain.Aggregates.Store.Orders.Entities;

// Domain/Entities/OrderStatusHistory.cs
public class OrderStatusHistory : Entity
{
    public long OrderId { get; private set; }
    public OrderStatusEnum Status { get; private set; }
    public string Note { get; private set; }
    public DateTime Timestamp { get; private set; }

    private OrderStatusHistory() { }

    public OrderStatusHistory(OrderStatusEnum status, string note)
    {
        Status = status;
        Note = note;
        Timestamp = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Note))
            throw new DomainException("Status note is required");
    }
}