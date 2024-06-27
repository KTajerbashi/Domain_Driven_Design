namespace Extensions.Events.Abstractions;

/// <summary>
/// الگوی 
/// Message Outbox
/// برای اینکه بتوانیم رویداد ها را تراکنش بس 
/// ثبت و پردازش و اجرا کنیم 
/// ازین الگو استفاده میکنیم
/// این الگو تمامی رویداد ها در جدول پایگاه داده ذخیره 
/// میکند و بعد شروع میکند به ارسال کردن 
/// رویداد ها جهت پردازش
/// </summary>
public interface IOutBoxEventItemRepository
{
    /// <summary>
    /// دریافت رویداد ها بصورت تعدادی برای اینکه 
    /// ارسال کنیم جهت پردازش
    /// </summary>
    /// <param name="maxCount"></param>
    /// <returns></returns>
    public List<OutBoxEventItem> GetOutBoxEventItemsForPublish(int maxCount = 100);
   
    /// <summary>
    /// رویداد های دریافتی را پردازش شده کند
    /// </summary>
    /// <param name="outBoxEventItems"></param>
    void MarkAsRead(List<OutBoxEventItem> outBoxEventItems);
}
