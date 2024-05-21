using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Core.Data.Sql.Commands.Library.Extensions;

/// <summary>
/// برای مدیریت همزمانی در دیتابیس میباشد
/// </summary>
public static class RowVersionShadowProperty
{
    public static readonly string RowVersion = nameof(RowVersion);

    public static void AddRowVersionShadowProperty<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
        => builder.Property<byte[]>(RowVersion).IsRowVersion();
}
