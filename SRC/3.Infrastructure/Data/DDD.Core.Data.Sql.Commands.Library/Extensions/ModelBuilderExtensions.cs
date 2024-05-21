using DDD.Core.Domain.Library.Entities;
using DDD.Core.Domain.Library.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Core.Data.Sql.Commands.Library.Extensions;
/// <summary>
/// 
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// تمام موجودیت ها را دنبال میکند و کلید بیزنیسی را در آنها قرار میدهد و ولیوکانورتور را برای آنها اعمال میکند
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void AddBusinessId(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model
                                               .GetEntityTypes()
                                               .Where(e => typeof(AggregateRoot).IsAssignableFrom(e.ClrType) ||
                                                    typeof(Entity).IsAssignableFrom(e.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType)
                .Property<BusinessId>("BusinessId").HasConversion(c => c.Value, d => BusinessId.FromGuid(d))
                .IsUnicode()
                .IsRequired();
            modelBuilder.Entity(entityType.ClrType).HasAlternateKey("BusinessId");
        }
    }
    /// <summary>
    /// ولیو کانورتور را روی تایپ خاصی اعمال میکند
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="modelBuilder"></param>
    /// <param name="converter"></param>
    /// <param name="maxLenght"></param>
    /// <returns></returns>
    public static ModelBuilder UseValueConverterForType<T>(this ModelBuilder modelBuilder, ValueConverter converter, int maxLenght = 0)
    {
        return modelBuilder.UseValueConverterForType(typeof(T), converter, maxLenght);
    }
    
    /// <summary>
    /// ولیو کانورتور را بای تایب اعمال میکند
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="type"></param>
    /// <param name="converter"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static ModelBuilder UseValueConverterForType(this ModelBuilder modelBuilder, Type type, ValueConverter converter, int maxLength = 0)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == type);

            foreach (var property in properties)
            {
                modelBuilder.Entity(entityType.Name).Property(property.Name)
                    .HasConversion(converter);
                if (maxLength > 0)
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasMaxLength(maxLength);
            }
        }

        return modelBuilder;
    }
}

