namespace TaskScheduler.EventBus.EventStore
{
    public interface IEventStoreRepository
    {
        void PublishEvent(IEvent @event);
    }
}