namespace Extensions.MessageBus.Abstractions;

public interface IReceiveMessageBus
{
    /// <summary>
    /// برای سرویس که میخواهد از یک مبدا 
    /// مشخص پیام دریافت کند
    /// </summary>
    /// <param name="serviceId"></param>
    /// <param name="eventName"></param>
    void Subscribe(string serviceId, string eventName);

    /// <summary>
    /// دریافت کننده یک رویداد
    /// </summary>
    void Receive(string commandName);
}
