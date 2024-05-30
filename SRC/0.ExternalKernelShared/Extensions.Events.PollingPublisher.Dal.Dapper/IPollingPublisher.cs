namespace Extensions.Events.PollingPublisher.Dal.Dapper;

public interface IPollingPublisher
{
    void Execute();
}
public abstract class PollingPublisher : IPollingPublisher
{
    public abstract void Execute();
}
