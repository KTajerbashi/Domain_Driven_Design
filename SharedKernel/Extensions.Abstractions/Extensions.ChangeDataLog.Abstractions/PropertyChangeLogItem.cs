using System;

namespace Extensions.ChangeDataLog.Abstractions;
/// <summary>
/// این یک موجودیت است که تغییرات هر پراپرتی رو
/// نگهداری میکند
/// </summary>
public class PropertyChangeLogItem
{
    /// <summary>
    /// کلید جدول
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    /// کلید پراپرتی
    /// </summary>
    public Guid ChangeInterceptorItemId { get; set; }
    /// <summary>
    /// نام پراپرتی
    /// </summary>
    public string PropertyName { get; set; }
    /// <summary>
    /// مقدار پراپرتی
    /// </summary>
    public string Value { get; set; }
}

