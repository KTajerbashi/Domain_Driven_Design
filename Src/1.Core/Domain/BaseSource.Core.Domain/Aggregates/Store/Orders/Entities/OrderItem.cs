namespace BaseSource.Core.Domain.Aggregates.Store.Orders.Entities;

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
