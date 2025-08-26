using BaseSource.Core.Domain.Aggregates.Store.Products.Events;
using BaseSource.Core.Domain.Aggregates.Store.Products.Parameters;
using BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;

namespace BaseSource.Core.Domain.Aggregates.Store.Products.Entities;

// Domain/Entities/Product.cs
[Table("Products", Schema = "Store")]
public class Product : AggregateRoot
{
    public Title Title { get; private set; }
    public Description Description { get; private set; }
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

    public Product(ProductCreateParameters parameter,
                  ProductCategory category, ProductWeight weight, ProductDimensions dimensions)
    {
        Title = parameter.Title;
        Description = parameter.Description;
        Price = parameter.Price;
        Sku = parameter.Sku;
        Category = category;
        Weight = weight;
        Dimensions = dimensions;
        IsActive = true;
        StockQuantity = 0;

        Validate();
        AddEvent(new ProductCreatedEvent(EntityId));
    }

    public void UpdateBasicInfo(string title, string description, decimal price)
    {
        Title = title;
        Description = description;
        Price = price;

        Validate();
        AddEvent(new ProductUpdatedEvent(EntityId));
    }

    public void UpdateStock(int quantity)
    {
        if (quantity < 0)
            throw new DomainException("Stock quantity cannot be negative");

        StockQuantity = quantity;
        AddEvent(new ProductStockUpdatedEvent(EntityId));
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public void AddDetail(string key, string value)
    {
        if (_details.Any(d => d.Key.Equals(key, StringComparison.OrdinalIgnoreCase)))
            throw new DomainException($"Detail with key '{key}' already exists");

        _details.Add(new ProductDetail(key, value));
        AddEvent(new ProductDetailAddedEvent(EntityId));
    }

    public void RemoveDetail(string key)
    {
        var detail = _details.FirstOrDefault(d => d.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        if (detail != null)
        {
            _details.Remove(detail);
            AddEvent(new ProductDetailDeletedEvent(EntityId));
        }
    }

    public void AddImage(string imageUrl, string altText, bool isMain = false)
    {
        if (isMain && _images.Any(img => img.IsMain))
            throw new DomainException("Main image already exists");

        _images.Add(new ProductImage(imageUrl, altText, isMain));
        AddEvent(new ProductImageAddedEvent(EntityId));
    }

    public void AddComment(long customerId, string title, string description, int rating)
    {
        if (rating < 1 || rating > 5)
            throw new DomainException("Rating must be between 1 and 5");

        _comments.Add(new ProductComment(customerId, Id, title, description, rating));
        AddEvent(new ProductCommentAddedEvent(EntityId));
    }

    public void RemoveComment(long commentId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment != null)
        {
            _comments.Remove(comment);
            AddEvent(new ProductCommentRemovedEvent(EntityId));
        }
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Title.ToString()))
            throw new DomainException("Product title is required");

        if (Price <= 0)
            throw new DomainException("Product price must be greater than 0");

        if (string.IsNullOrWhiteSpace(Sku))
            throw new DomainException("Product SKU is required");
    }
}
