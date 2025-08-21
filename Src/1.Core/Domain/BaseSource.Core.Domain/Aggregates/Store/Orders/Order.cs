using BaseSource.Core.Domain.Aggregates.Store.Products.Entities;

namespace BaseSource.Core.Domain.Aggregates.Store.Orders;

// Domain/Entities/Order.cs
public class Order : AggregateRoot
{
    public long CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
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
        Status = OrderStatus.Pending;
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
        if (Status != OrderStatus.Pending)
            throw new DomainException("Only pending orders can be confirmed");

        Status = OrderStatus.Confirmed;
        _statusHistory.Add(new OrderStatusHistory(Status, "Payment confirmed"));
    }

    public void Process()
    {
        if (Status != OrderStatus.Confirmed)
            throw new DomainException("Only confirmed orders can be processed");

        Status = OrderStatus.Processing;
        _statusHistory.Add(new OrderStatusHistory(Status, "Order processing started"));
    }

    public void Ship()
    {
        if (Status != OrderStatus.Processing)
            throw new DomainException("Only processing orders can be shipped");

        Status = OrderStatus.Shipped;
        _statusHistory.Add(new OrderStatusHistory(Status, "Order shipped"));
    }

    public void Deliver()
    {
        if (Status != OrderStatus.Shipped)
            throw new DomainException("Only shipped orders can be delivered");

        Status = OrderStatus.Delivered;
        _statusHistory.Add(new OrderStatusHistory(Status, "Order delivered"));
    }

    public void Cancel(string reason)
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled)
            throw new DomainException("Cannot cancel delivered or already cancelled order");

        Status = OrderStatus.Cancelled;
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

public enum OrderStatus
{
    Pending,
    Confirmed,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

// Domain/ValueObjects/Address.cs
public record Address
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; }
    public string ZipCode { get; }

    public Address(string street, string city, string state, string country, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Street))
            throw new DomainException("Street is required");

        if (string.IsNullOrWhiteSpace(City))
            throw new DomainException("City is required");

        if (string.IsNullOrWhiteSpace(Country))
            throw new DomainException("Country is required");
    }
}

// Domain/Entities/OrderItem.cs
public class OrderItem : Entity
{
    public long OrderId { get; private set; }
    public long ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice => UnitPrice * Quantity;

    private OrderItem() { }

    public OrderItem(long productId, string productName, decimal unitPrice, int quantity)
    {
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;

        Validate();
    }

    private void Validate()
    {
        if (ProductId <= 0)
            throw new DomainException("Product ID is required");

        if (string.IsNullOrWhiteSpace(ProductName))
            throw new DomainException("Product name is required");

        if (UnitPrice <= 0)
            throw new DomainException("Unit price must be greater than 0");

        if (Quantity <= 0)
            throw new DomainException("Quantity must be greater than 0");
    }
}

// Domain/Entities/OrderStatusHistory.cs
public class OrderStatusHistory : Entity
{
    public long OrderId { get; private set; }
    public OrderStatus Status { get; private set; }
    public string Note { get; private set; }
    public DateTime Timestamp { get; private set; }

    private OrderStatusHistory() { }

    public OrderStatusHistory(OrderStatus status, string note)
    {
        Status = status;
        Note = note;
        Timestamp = DateTime.UtcNow;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Note))
            throw new DomainException("Status note is required");
    }
}