namespace Extensions.Events.PollingPublisher.Options;

public class PollingPublisherOptions
{
    /// <summary>
    /// هر چند ملی ثانیه رویداد ارسال کن
    /// </summary>
    public int SendInterval { get; set; } = 1000;
    
    /// <summary>
    /// اگر استثنا داشت بعد تعداد ثانیه تعریف شده ارسال کن
    /// </summary>
    public int ExceptionInterval { get; set; } = 10000;
    
    /// <summary>
    /// تعداد رویداد های که بخونه تعیین میکند
    /// چند تا چند تا رویداد بردار بیار و اجرا کند
    /// </summary>
    public int SendCount { get; set; } = 100;

    /// <summary>
    /// نام اپلیکشن که قرار است اطلاعات برایش در صف قرار دهیم
    /// </summary>
    public string ApplicationName { get; set; }
}
