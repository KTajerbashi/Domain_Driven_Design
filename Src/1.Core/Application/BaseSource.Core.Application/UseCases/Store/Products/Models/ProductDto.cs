namespace BaseSource.Core.Application.UseCases.Store.Products.Models;

public class ProductDto : BaseDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string CategorySlug { get; set; } = string.Empty;
    public decimal WeightValue { get; set; }
    public string WeightUnit { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public string DimensionUnit { get; set; } = string.Empty;
    public double AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public string MainImageUrl { get; set; } = string.Empty;
}

public class ProductSummaryDto : BaseDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string MainImageUrl { get; set; } = string.Empty;
    public double AverageRating { get; set; }
    public bool InStock { get; set; }
}
