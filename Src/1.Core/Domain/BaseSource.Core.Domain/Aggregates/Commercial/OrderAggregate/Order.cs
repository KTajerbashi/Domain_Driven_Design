using BaseSource.Core.Domain.Aggregates.Commercial.ProductAggregate;

namespace BaseSource.Core.Domain.Aggregates.Commercial.OrderAggregate;

public class Order : AggregateRoot
{
    private List<OrderItem> _orderItems;
    public virtual List<OrderItem> OrderItems => _orderItems;
}

public class OrderItem : AuditableEntity
{
    public string Title { get; set; }
    public int Count { get; set; }
}