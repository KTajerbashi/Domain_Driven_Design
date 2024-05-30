namespace Extensions.MessageBus.MessageInbox.Dal.Dapper;

public interface IMessageBusMessageInbox
{
    void Execute();
}
public abstract class MessageBusMessageInbox : IMessageBusMessageInbox
{
    public abstract void Execute();
   
}