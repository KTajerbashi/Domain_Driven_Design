namespace BaseSource.Core.Domain.Aggregates.Store.Products.Parameters;

public record ProductCreateParameters(string Title, string Description, decimal Price, string Sku);
