using BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;

namespace BaseSource.Core.Domain.Aggregates.Store.Products.Entities;

// Domain/Entities/Product.cs
public class Product : AggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public string Sku { get; private set; }

    // Value Objects
    public ProductCategory Category { get; private set; }
    public ProductWeight Weight { get; private set; }
    public ProductDimensions Dimensions { get; private set; }

    // Collections
    private readonly List<ProductDetail> _details = new();
    public IReadOnlyCollection<ProductDetail> Details => _details.AsReadOnly();

    private readonly List<ProductImage> _images = new();
    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

    private readonly List<ProductComment> _comments = new();
    public IReadOnlyCollection<ProductComment> Comments => _comments.AsReadOnly();

    private Product() { } // For EF Core

    public Product(string title, string description, decimal price, string sku,
                  ProductCategory category, ProductWeight weight, ProductDimensions dimensions)
    {
        Title = title;
        Description = description;
        Price = price;
        Sku = sku;
        Category = category;
        Weight = weight;
        Dimensions = dimensions;
        IsActive = true;
        StockQuantity = 0;

        Validate();
    }

    public void UpdateBasicInfo(string title, string description, decimal price)
    {
        Title = title;
        Description = description;
        Price = price;

        Validate();
    }

    public void UpdateStock(int quantity)
    {
        if (quantity < 0)
            throw new DomainException("Stock quantity cannot be negative");

        StockQuantity = quantity;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public void AddDetail(string key, string value)
    {
        if (_details.Any(d => d.Key.Equals(key, StringComparison.OrdinalIgnoreCase)))
            throw new DomainException($"Detail with key '{key}' already exists");

        _details.Add(new ProductDetail(key, value));
    }

    public void RemoveDetail(string key)
    {
        var detail = _details.FirstOrDefault(d => d.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        if (detail != null)
            _details.Remove(detail);
    }

    public void AddImage(string imageUrl, string altText, bool isMain = false)
    {
        if (isMain && _images.Any(img => img.IsMain))
            throw new DomainException("Main image already exists");

        _images.Add(new ProductImage(imageUrl, altText, isMain));
    }

    public void AddComment(long customerId, string title, string description, int rating)
    {
        if (rating < 1 || rating > 5)
            throw new DomainException("Rating must be between 1 and 5");

        _comments.Add(new ProductComment(customerId, Id, title, description, rating));
    }

    public void RemoveComment(long commentId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment != null)
            _comments.Remove(comment);
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new DomainException("Product title is required");

        if (Price <= 0)
            throw new DomainException("Product price must be greater than 0");

        if (string.IsNullOrWhiteSpace(Sku))
            throw new DomainException("Product SKU is required");
    }
}




// Domain/Entities/ProductDetail.cs
public class ProductDetail : Entity
{
    public long ProductId { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }

    private ProductDetail() { }

    public ProductDetail(string key, string value)
    {
        Key = key;
        Value = value;

        Validate();
    }

    public void UpdateValue(string value)
    {
        Value = value;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Key))
            throw new DomainException("Detail key is required");

        if (string.IsNullOrWhiteSpace(Value))
            throw new DomainException("Detail value is required");
    }
}

// Domain/Entities/ProductImage.cs
public class ProductImage : Entity
{
    public long ProductId { get; private set; }
    public string ImageUrl { get; private set; }
    public string AltText { get; private set; }
    public bool IsMain { get; private set; }
    public int DisplayOrder { get; private set; }

    private ProductImage() { }

    public ProductImage(string imageUrl, string altText, bool isMain = false, int displayOrder = 0)
    {
        ImageUrl = imageUrl;
        AltText = altText;
        IsMain = isMain;
        DisplayOrder = displayOrder;

        Validate();
    }

    public void SetAsMain()
    {
        IsMain = true;
    }

    public void RemoveAsMain()
    {
        IsMain = false;
    }

    public void UpdateDisplayOrder(int order)
    {
        DisplayOrder = order;
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(ImageUrl))
            throw new DomainException("Image URL is required");
    }
}

// Domain/Entities/ProductComment.cs
public class ProductComment : Entity
{
    public long CustomerId { get; private set; }
    public long ProductId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int Rating { get; private set; }
    public bool IsApproved { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private ProductComment() { }

    public ProductComment(long customerId, long productId, string title, string description, int rating)
    {
        CustomerId = customerId;
        ProductId = productId;
        Title = title;
        Description = description;
        Rating = rating;
        IsApproved = false;
        CreatedAt = DateTime.UtcNow;

        Validate();
    }

    public void Approve() => IsApproved = true;
    public void Reject() => IsApproved = false;

    public void UpdateComment(string title, string description, int rating)
    {
        Title = title;
        Description = description;
        Rating = rating;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new DomainException("Comment title is required");

        if (string.IsNullOrWhiteSpace(Description))
            throw new DomainException("Comment description is required");

        if (Rating < 1 || Rating > 5)
            throw new DomainException("Rating must be between 1 and 5");
    }
}

// Domain/Entities/ShoppingCart.cs
public class ShoppingCart : AggregateRoot
{
    public long CustomerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<CartItem> _items = new();
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public decimal TotalPrice => _items.Sum(item => item.TotalPrice);
    public int TotalItems => _items.Sum(item => item.Quantity);

    private ShoppingCart() { }

    public ShoppingCart(long customerId)
    {
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
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
            _items.Add(new CartItem(product.Id, product.Title, product.Price, quantity));
        }

        UpdatedAt = DateTime.UtcNow;
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

        UpdatedAt = DateTime.UtcNow;
    }

    public void ClearCart()
    {
        _items.Clear();
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateItemQuantity(long productId, int newQuantity)
    {
        if (newQuantity <= 0)
            throw new DomainException("Quantity must be greater than 0");

        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null)
            throw new DomainException("Item not found in cart");

        item.UpdateQuantity(newQuantity);
        UpdatedAt = DateTime.UtcNow;
    }
}

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