using BaseSource.Core.Domain.Aggregates.Store.Products.Entities;

namespace BaseSource.Core.Domain.Aggregates.Store.Carts.Entities;

// Domain/Entities/ShoppingCart.cs
public class ShoppingCart : AggregateRoot
{
    public long CustomerId { get; private set; }

    private readonly List<CartItem> _items = new();
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public decimal TotalPrice => _items.Sum(item => item.TotalPrice);
    public int TotalItems => _items.Sum(item => item.Quantity);

    private ShoppingCart() { }

    public ShoppingCart(long customerId)
    {
        CustomerId = customerId;
    }

    public void AddItem(Product product, int quantity = 1)
    {
        if (product == null)
            throw new DomainException("Product is required");

        if (!product.IsActive)
            throw new DomainException("Product is not available");

        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than 0");

        if (product.StockQuantity < quantity)
            throw new DomainException("Insufficient stock");

        var existingItem = _items.FirstOrDefault(item => item.ProductId == product.Id);
        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
        }
        else
        {
            _items.Add(new CartItem(product.Id, product.Title.ToString(), product.Price, quantity));
        }

    }

    public void RemoveItem(long productId, int quantity = 1)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null)
            throw new DomainException("Item not found in cart");

        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than 0");

        if (quantity >= item.Quantity)
        {
            _items.Remove(item);
        }
        else
        {
            item.DecreaseQuantity(quantity);
        }

    }

    public void ClearCart()
    {
        _items.Clear();
    }

    public void UpdateItemQuantity(long productId, int newQuantity)
    {
        if (newQuantity <= 0)
            throw new DomainException("Quantity must be greater than 0");

        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null)
            throw new DomainException("Item not found in cart");

        item.UpdateQuantity(newQuantity);
    }
}
