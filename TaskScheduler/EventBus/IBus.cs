namespace TaskScheduler.EventBus
{
    public interface IBus
    {
        void Publish<T>(T @event) where T : IEvent;
    }
}