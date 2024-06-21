namespace Extensions.Events.Abstractions;


public class OutBoxEventItem
{
    /// <summary>
    /// کلید رخداد
    /// </summary>
    public long OutBoxEventItemId { get; set; }
    /// <summary>
    /// کلید رخداد
    /// </summary>
    public Guid EventId { get; set; }
    /// <summary>
    /// توسط کدام کاربر رخ داد
    /// </summary>
    public string? AccruedByUserId { get; set; }
    /// <summary>
    /// چه زمانی رخ داده است
    /// </summary>
    public DateTime AccruedOn { get; set; }
    /// <summary>
    /// برای چه شاخه بوده
    /// </summary>
    public string AggregateName { get; set; }
    /// <summary>
    /// نوع شاخه چی بوده
    /// </summary>
    public string AggregateTypeName { get; set; }
    /// <summary>
    /// شناسه شاخه چی بوده
    /// </summary>
    public string AggregateId { get; set; }
    /// <summary>
    /// اسم رویداد
    /// </summary>
    public string EventName { get; set; }
    /// <summary>
    /// نوع رویداد که اتفاق افتاد
    /// </summary>
    public string EventTypeName { get; set; }
    /// <summary>
    /// دیتا چی بوده
    /// </summary>
    public string EventPayload { get; set; }
    /// <summary>
    /// رهگیری
    /// </summary>
    public string? TraceId { get; set; }
    /// <summary>
    /// رهگیری
    /// </summary>
    public string? SpanId { get; set; }
    /// <summary>
    /// آیا پردازش شده است
    /// </summary>
    public bool IsProcessed { get; set; }
}

