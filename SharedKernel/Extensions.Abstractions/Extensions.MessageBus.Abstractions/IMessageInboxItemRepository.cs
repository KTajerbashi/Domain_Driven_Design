namespace Extensions.MessageBus.Abstractions;

/// <summary>
/// الگوی 
/// Message Inbox
/// برای مدیریت پیامی که اگر چند بار ارسال شود آنرا
/// فقط یکبار پیاده سازی و اجرا کند
/// </summary>
public interface IMessageInboxItemRepository
{
    /// <summary>
    /// بررسی آیا این پیامی که دریافت کردم را میتوانم 
    /// پردازش کنم یا خیر
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="fromService"></param>
    /// <returns></returns>
    bool AllowReceive(string messageId, string fromService);
    
    /// <summary>
    /// دریافت یک پیامی از سرویس  با پیامی که دارد
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="fromService"></param>
    /// <param name="payload"></param>
    void Receive(string messageId, string fromService, string payload);
}
