using BaseSource.Core.Domain.Aggregates.Store.Carts.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Customers.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Orders.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Products.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Infrastructure.SQL.Command.Configurations.Store;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems", "Store");
        builder.HasKey(p => p.Id);
    }
}


public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.ToTable("ShoppingCarts", "Store");
        builder.HasKey(p => p.Id);
    }
}


public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers", "Store");
        builder.HasKey(p => p.Id);
    }
}
public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.ToTable("CustomerAddresses", "Store");
        builder.HasKey(p => p.Id);
    }
}



public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "Store");
        builder.HasKey(p => p.Id);
    }
}



public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems", "Store");
        builder.HasKey(p => p.Id);
    }
}



public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistory>
{
    public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
    {
        builder.ToTable("OrderStatusHistories", "Store");
        builder.HasKey(p => p.Id);
    }
}


public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "Store");
        builder.HasKey(p => p.Id);
    }
}

public class ProductCommentConfiguration : IEntityTypeConfiguration<ProductComment>
{
    public void Configure(EntityTypeBuilder<ProductComment> builder)
    {
        builder.ToTable("ProductComments", "Store");
        builder.HasKey(p => p.Id);
    }
}

public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
{
    public void Configure(EntityTypeBuilder<ProductDetail> builder)
    {
        builder.ToTable("ProductDetails", "Store");
        builder.HasKey(p => p.Id);
    }
}

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages", "Store");
        builder.HasKey(p => p.Id);
    }
}
