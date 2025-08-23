namespace BaseSource.Core.Domain.Aggregates.Store.Products.Entities;

// Domain/Entities/ProductImage.cs
[Table("ProductImages", Schema = "Store")]
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
