using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Infra.Data.Sql.Commands.Library.Extensions;

/// <summary>
/// مدیریت همزمانی در پایگاه داده کاربرد دارد
/// نسخه برای ردیف در لایه دیتابیس است
/// 
/// </summary>
public static class RowVersionShadowProperty
{
    public static readonly string RowVersion = nameof(RowVersion);

    /// <summary>
    /// رو ورژن بصورت شادو پراپرتی خواستیم استفاده کنیم ازین متد استفاده میکنیم
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="builder"></param>
    public static void AddRowVersionShadowProperty<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class
        => builder.Property<byte[]>(RowVersion).IsRowVersion();
}
