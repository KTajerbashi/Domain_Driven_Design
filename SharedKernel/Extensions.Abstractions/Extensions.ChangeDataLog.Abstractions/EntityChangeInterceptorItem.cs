namespace Extensions.ChangeDataLog.Abstractions;
public class EntityChangeInterceptorItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContextName { get; set; }
    public string EntityType { get; set; }
    public string EntityId { get; set; }
    public string UserId { get; set; }
    public string Ip { get; set; }
    public string TransactionId { get; set; }
    public DateTime DateOfOccurrence { get; set; }
    public string ChangeType { get; set; }
    /// <summary>
    /// این لیست پراپرتی های که تغییر کرده را در خودش نگه میدارد
    /// </summary>
    public List<PropertyChangeLogItem> PropertyChangeLogItems { get; set; } = new List<PropertyChangeLogItem>();
    public void AddPropertyChangeItem(string propertyName, string value)
    {
        PropertyChangeLogItems.Add(new PropertyChangeLogItem
        {
            ChageInterceptorItemId = Id,
            PropertyName = propertyName,
            Value = value
        });
    }
}
