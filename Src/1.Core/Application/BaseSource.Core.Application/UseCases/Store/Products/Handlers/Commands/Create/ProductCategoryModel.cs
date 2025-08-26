using BaseSource.Core.Domain.Aggregates.Store.Products.Enums;
using BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Commands.Create;

public class ProductCategoryRoot
{
    public record ProductDetailModel(string Key, string Value);
    public class ProductDimensionsModel
    {
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public LengthUnitEnum Unit { get; set; }
        internal ProductDimensions ValueObject() => new ProductDimensions(Length, Width, Height, Unit);
    }

    public class ProductWeightModel
    {
        public decimal Value { get; set; }
        public WeightUnitEnum WeightUnit { get; set; }
        internal ProductWeight ValueObject() => new ProductWeight(Value, WeightUnit);
    }


    public class ProductCategoryModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public long? ParentCategoryId { get; set; }

        internal ProductCategory ValueObject() => new ProductCategory(Name, Slug, ParentCategoryId);
    }
}

