using DDD.Core.Domain.Library.Entities;
using DDD.Core.Domain.Library.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Infra.Data.Sql.Commands.Library.Extensions;
/// <summary>
/// model builder  زمانی که یک کلاس در ای اف میخواهد ساخته شود نقش آفرینی میکند
/// ,
/// در نسخه جدید 
/// EF
/// امکان تعبیه شده است که بتوانیم کانویشن توکار را نیز تغییر بدهیم دیگر شاید لازم
/// نباشد ازین سرویس ها استفاده کنیم
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// این متد برای اضافه کردن ولیوآبجکت کلید بیزنسی است که بعد از 
    /// Value Conversions
    /// دیگه نیازی به این روش پیاده سازی نیست
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void AddBusinessId(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model
                                               .GetEntityTypes()
                                               .Where(e => 
                                               typeof(AggregateRoot).IsAssignableFrom(e.ClrType) ||
                                               typeof(Entity).IsAssignableFrom(e.ClrType)
                                               )
                )
        {
            modelBuilder
                .Entity(entityType.ClrType)
                .Property<BusinessId>("BusinessId")
                .HasConversion(c => c.Value, d => BusinessId.FromGuid(d))
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity(entityType.ClrType)
                .HasAlternateKey("BusinessId");
        }
    }
    
    /// <summary>
    /// توسط این متد میتوانیم 
    /// Value Convertor 
    /// هارا بصورت تایب مشخص ست کنیم
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
    /// توسط این متد میتوانیم
    /// value convertor 
    /// مورد نظر را برایش ارسال کنیم و ست کنیم
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

