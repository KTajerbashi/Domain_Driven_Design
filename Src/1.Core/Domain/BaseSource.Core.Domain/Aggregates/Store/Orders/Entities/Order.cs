using BaseSource.Core.Domain.Aggregates.Store.Carts.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Orders.Enums;
using BaseSource.Core.Domain.Aggregates.Store.Orders.ValueObjects;

namespace BaseSource.Core.Domain.Aggregates.Store.Orders.Entities;

// Domain/Entities/Order.cs
public class Order : AggregateRoot
{
    public long CustomerId { get; private set; }
    public OrderStatusEnum Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime OrderDate { get; private set; }
    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private readonly List<OrderStatusHistory> _statusHistory = new();
    public IReadOnlyCollection<OrderStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    private Order() { }

    public Order(long customerId, Address shippingAddress, Address billingAddress, List<CartItem> cartItems)
    {
        CustomerId = customerId;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Status = OrderStatusEnum.Pending;
        OrderDate = DateTime.UtcNow;

        foreach (var cartItem in cartItems)
        {
            _items.Add(new OrderItem(
                cartItem.ProductId,
                cartItem.ProductName,
                cartItem.UnitPrice,
                cartItem.Quantity
            ));
        }

        TotalAmount = _items.Sum(item => item.TotalPrice);
        _statusHistory.Add(new OrderStatusHistory(Status, "Order created"));

        Validate();
    }

    public void ConfirmPayment()
    {
        if (Status != OrderStatusEnum.Pending)
            throw new DomainException("Only pending orders can be confirmed");

        Status = OrderStatusEnum.Confirmed;
        _statusHistory.Add(new OrderStatusHistory(Status, "Payment confirmed"));
    }

    public void Process()
    {
        if (Status != OrderStatusEnum.Confirmed)
            throw new DomainException("Only confirmed orders can be processed");

        Status = OrderStatusEnum.Processing;
        _statusHistory.Add(new OrderStatusHistory(Status, "Order processing started"));
    }

    public void Ship()
    {
        if (Status != OrderStatusEnum.Processing)
            throw new DomainException("Only processing orders can be shipped");

        Status = OrderStatusEnum.Shipped;
        _statusHistory.Add(new OrderStatusHistory(Status, "Order shipped"));
    }

    public void Deliver()
    {
        if (Status != OrderStatusEnum.Shipped)
            throw new DomainException("Only shipped orders can be delivered");

        Status = OrderStatusEnum.Delivered;
        _statusHistory.Add(new OrderStatusHistory(Status, "Order delivered"));
    }

    public void Cancel(string reason)
    {
        if (Status == OrderStatusEnum.Delivered || Status == OrderStatusEnum.Cancelled)
            throw new DomainException("Cannot cancel delivered or already cancelled order");

        Status = OrderStatusEnum.Cancelled;
        _statusHistory.Add(new OrderStatusHistory(Status, $"Order cancelled: {reason}"));
    }

    public void AddNote(string note)
    {
        _statusHistory.Add(new OrderStatusHistory(Status, note));
    }

    private void Validate()
    {
        if (CustomerId <= 0)
            throw new DomainException("Customer ID is required");

        if (_items.Count == 0)
            throw new DomainException("Order must have at least one item");

        if (TotalAmount <= 0)
            throw new DomainException("Total amount must be greater than 0");
    }
}

