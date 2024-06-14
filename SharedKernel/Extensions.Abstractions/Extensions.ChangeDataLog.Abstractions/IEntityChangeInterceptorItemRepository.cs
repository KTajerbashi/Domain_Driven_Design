namespace Extensions.ChangeDataLog.Abstractions;
/// <summary>
/// تغییراتی که روی دتابس انجام میشود رو بر اساس داده های که آمده و تغییر کرده بر اساس چه کاربر و چه تراکنشی رصد میکنیم
/// </summary>
public interface IEntityChangeInterceptorItemRepository
{
    /// <summary>
    /// ذخیره تغییرات روی موجودیت
    /// </summary>
    /// <param name="entityChangeInterceptorItems"></param>
    public void Save(List<EntityChangeInterceptorItem> entityChangeInterceptorItems);
    
    /// <summary>
    /// ذخیره تغییرات روی موجودیت
    /// </summary>
    /// <param name="entityChangeInterceptorItems"></param>
    /// <returns></returns>
    public Task SaveAsync(List<EntityChangeInterceptorItem> entityChangeInterceptorItems);
}
