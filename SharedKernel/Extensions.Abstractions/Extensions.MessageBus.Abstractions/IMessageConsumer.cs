namespace Extensions.MessageBus.Abstractions;

public interface IMessageConsumer
{
    /// <summary>
    /// زمانی که یک دستوری دریافت میشود بعد از آن این 
    /// متد آنرا دریافت کرده و در صف قرار میدهد
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="parcel"></param>
    /// <returns></returns>
    Task<bool> ConsumeEvent(string sender, Parcel parcel);

    /// <summary>
    /// زمانی که یک رویداد دریافت میشود بعد از آن این 
    /// متد آنرا دریافت کرده و در صف قرار میدهد
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="parcel"></param>
    /// <returns></returns>
    Task<bool> ConsumeCommand(string sender, Parcel parcel);
}
