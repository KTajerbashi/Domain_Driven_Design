namespace BaseSource.Core.Domain.Aggregates.Store.Products.Entities;

// Domain/Entities/ProductComment.cs

[Table("ProductComments", Schema ="Store")]
public class ProductComment : Entity
{
    public long CustomerId { get; private set; }
    public long ProductId { get; private set; }
    public Title Title { get; private set; }
    public Description Description { get; private set; }
    public int Rating { get; private set; }
    public bool IsApproved { get; private set; }

    private ProductComment() { }

    public ProductComment(long customerId, long productId, string title, string description, int rating)
    {
        CustomerId = customerId;
        ProductId = productId;
        Title = title;
        Description = description;
        Rating = rating;
        IsApproved = false;
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
        if (string.IsNullOrWhiteSpace(Title.ToString()))
            throw new DomainException("Comment title is required");

        if (string.IsNullOrWhiteSpace(Description.ToString()))
            throw new DomainException("Comment description is required");

        if (Rating < 1 || Rating > 5)
            throw new DomainException("Rating must be between 1 and 5");
    }
}
