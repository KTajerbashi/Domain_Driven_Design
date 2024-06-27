namespace Extensions.ChangeDataLog.Abstractions;

/// <summary>
/// موجودیت که تغییرات روی آن اعمال میشود
/// </summary>
public class EntityChangeInterceptorItem
{
    /// <summary>
    /// کلید جدول
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    /// نام کانتکس
    /// </summary>
    public string ContextName { get; set; }
    /// <summary>
    /// نوع موجودیت
    /// </summary>
    public string EntityType { get; set; }
    /// <summary>
    /// کلید موجودیت
    /// </summary>
    public string EntityId { get; set; }
    /// <summary>
    /// کلید کاربر جاری
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// آی پی درخواست
    /// </summary>
    public string Ip { get; set; }
    /// <summary>
    /// کلید تراکنش
    /// </summary>
    public string TransactionId { get; set; }
    /// <summary>
    /// چه تاریخی اتفاق افتاده است
    /// </summary>
    public DateTime DateOfOccurrence { get; set; }
    /// <summary>
    /// نوع تغییری که اتفاق افتاده است
    /// </summary>
    public string ChangeType { get; set; }
    /// <summary>
    /// این لیست پراپرتی های که تغییر کرده را در خودش نگه میدارد
    /// </summary>
    public List<PropertyChangeLogItem> PropertyChangeLogItems { get; set; } = new List<PropertyChangeLogItem>();

    public void AddPropertyChangeItem(string propertyName, string value)
    {
        PropertyChangeLogItems.Add(new PropertyChangeLogItem
        {
            ChangeInterceptorItemId = Id,
            PropertyName = propertyName,
            Value = value
        });
    }
}
