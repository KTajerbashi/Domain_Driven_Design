namespace Extensions.MessageBus.Abstractions;
/// <summary>
/// این کلاس در نرم‌افزار داده‌هایی که باید از طریق زیر ساخت پیام رسانی منتقل شود را نگهداری می‌کند
/// </summary>
public class Parcel
{
    /// <summary>
    /// کلید پیام
    /// </summary>
    public string MessageId { get; set; } = string.Empty;
    /// <summary>
    /// شناسه یک شی وابسته
    /// </summary>
    public string CorrelationId { get; set; } = string.Empty;
    /// <summary>
    /// اسم پیامی که میفرستیم
    /// </summary>
    public string MessageName { get; set; } = string.Empty;
    /// <summary>
    /// هیدر های که برای پیام میگذاریم
    /// </summary>
    public Dictionary<string, object> Headers { get; set; } = new Dictionary<string, object>();
    /// <summary>
    /// بدنه پیامی که ارسال میکنیم
    /// </summary>
    public string MessageBody { get; set; } = string.Empty;
    /// <summary>
    /// روت پیامی که باید ارسال شود
    /// </summary>
    public string Route { get; set; } = string.Empty;
}
