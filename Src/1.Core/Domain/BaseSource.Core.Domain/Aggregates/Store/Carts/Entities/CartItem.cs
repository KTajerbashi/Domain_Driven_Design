namespace BaseSource.Core.Domain.Aggregates.Store.Carts.Entities;

// Domain/Entities/CartItem.cs
public class CartItem : Entity
{
    public long ShoppingCartId { get; private set; }
    public long ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice => UnitPrice * Quantity;

    private CartItem() { }

    public CartItem(long productId, string productName, decimal unitPrice, int quantity)
    {
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;

        Validate();
    }

    public void IncreaseQuantity(int amount = 1)
    {
        if (amount <= 0)
            throw new DomainException("Amount must be greater than 0");

        Quantity += amount;
    }

    public void DecreaseQuantity(int amount = 1)
    {
        if (amount <= 0)
            throw new DomainException("Amount must be greater than 0");

        if (amount >= Quantity)
            throw new DomainException("Cannot decrease below zero");

        Quantity -= amount;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new DomainException("Quantity must be greater than 0");

        Quantity = newQuantity;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new DomainException("Price must be greater than 0");

        UnitPrice = newPrice;
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