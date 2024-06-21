using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System.Globalization;
using DDD.Infra.Data.Sql.Commands.Library.ValueConversions;
using DDD.Infra.Data.Sql.Commands.Library.Extensions;
using DDD.Core.Domain.ToolKits.Library.ValueObjects;
using DDD.Core.Domain.Library.ValueObjects;

namespace DDD.Infra.Data.Sql.Commands.Library;
/// <summary>
/// 
/// </summary>
public abstract class BaseCommandDbContext : DbContext
{
    protected IDbContextTransaction _transaction;

    public BaseCommandDbContext(DbContextOptions options) : base(options)
    {

    }

    protected BaseCommandDbContext()
    {
    }
    /// <summary>
    /// شروع تراکنش
    /// </summary>
    public void BeginTransaction()
    {
        _transaction = Database.BeginTransaction();
    }
    /// <summary>
    /// برگشت تراکنش
    /// </summary>
    /// <exception cref="NullReferenceException"></exception>
    public void RollbackTransaction()
    {
        if (_transaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }
        _transaction.Rollback();
    }
    /// <summary>
    /// کامیت کردن تراکنش
    /// </summary>
    /// <exception cref="NullReferenceException"></exception>
    public void CommitTransaction()
    {
        if (_transaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }
        _transaction.Commit();
    }
    /// <summary>
    /// دریافت مقدار شادو پراپرتی
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
    {
        var value = Entry(entity).Property(propertyName).CurrentValue;
        return value != null
            ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
            : default;
    }

    /// <summary>
    /// دریافت مقدار شادو پراپرتی
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public object GetShadowPropertyValue(object entity, string propertyName)
    {
        return Entry(entity).Property(propertyName).CurrentValue;
    }

    /// <summary>
    /// مطمیین شویم شادو پراپرتی ها به مدل دتابیس ما وصل میشود
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.AddAuditableShadowProperties();
    }

    /// <summary>
    /// اعمال شدن ولیو کانورتور
    /// </summary>
    /// <param name="configurationBuilder"></param>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<Description>().HaveConversion<DescriptionConversion>();
        configurationBuilder.Properties<Title>().HaveConversion<TitleConversion>();
        configurationBuilder.Properties<BusinessId>().HaveConversion<BusinessIdConversion>();
        configurationBuilder.Properties<LegalNationalId>().HaveConversion<LegalNationalId>();
        configurationBuilder.Properties<NationalCode>().HaveConversion<NationalCodeConversion>();

    }

    /// <summary>
    /// فرزندان یک موجودیت شاخه را کامل برگرداند
    /// دریافت Include
    /// .ThenInclude
    /// </summary>
    /// <param name="clrEntityType"></param>
    /// <returns></returns>
    public IEnumerable<string> GetIncludePaths(Type clrEntityType)
    {
        var entityType = Model.FindEntityType(clrEntityType);
        var includedNavigations = new HashSet<INavigation>();
        var stack = new Stack<IEnumerator<INavigation>>();
        while (true)
        {
            var entityNavigations = new List<INavigation>();
            foreach (var navigation in entityType.GetNavigations())
            {
                if (includedNavigations.Add(navigation))
                    entityNavigations.Add(navigation);
            }
            if (entityNavigations.Count == 0)
            {
                if (stack.Count > 0)
                    yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
            }
            else
            {
                foreach (var navigation in entityNavigations)
                {
                    var inverseNavigation = navigation.Inverse;
                    if (inverseNavigation != null)
                        includedNavigations.Add(inverseNavigation);
                }
                stack.Push(entityNavigations.GetEnumerator());
            }
            while (stack.Count > 0 && !stack.Peek().MoveNext())
                stack.Pop();
            if (stack.Count == 0) break;
            entityType = stack.Peek().Current.TargetEntityType;
        }
    }
}