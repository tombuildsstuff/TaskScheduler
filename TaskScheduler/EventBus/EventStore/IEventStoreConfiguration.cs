namespace TaskScheduler.EventBus.EventStore
{
    public interface IEventStoreConfiguration
    {
        string IpAddress { get; }
        int Port { get; }
        string UserName { get; }
        string Password { get; }
    }
}