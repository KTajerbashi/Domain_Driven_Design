using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Zamin.Extensions.UsersManagement.Abstractions;
using DDD.Core.Data.Sql.Commands.Library.Extensions;

namespace DDD.Core.Data.Sql.Commands.Library.Interceptors;

//// اینترسپتور ها برای ایجاد یک پاپلاین جهت پیاده سازی منطق بدون نیاز به دوباره نویسی متد های دیگر برای پایگاه داده شده است
//// هنگام ذخیره سازی اینتر سپتور های اعمال میشوند

/// <summary>
/// زمانی آدیت فیلد هارا میخواهیم ست کنیم میخواهیم قبل ذخیره سازی ست شوند
/// </summary>
public class AddAuditDataInterceptor : SaveChangesInterceptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AddShadowProperties(eventData);
        return base.SavingChanges(eventData, result);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        AddShadowProperties(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    private static void AddShadowProperties(DbContextEventData eventData)
    {
        var changeTracker = eventData.Context.ChangeTracker;
        var userInfoService = eventData.Context.GetService<IUserInfoService>();
        changeTracker.SetAuditableEntityPropertyValues(userInfoService);
    }
}
