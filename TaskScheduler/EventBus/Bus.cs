namespace TaskScheduler.EventBus
{
    public class Bus : IBus
    {
        private readonly IEventHandlerFactory _eventFactory;
        public static IBus Instance { get; private set; }

        private Bus(IEventHandlerFactory eventFactory)
        {
            _eventFactory = eventFactory;
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            _eventFactory.GetInstanceOf<T>().Handle(@event);
        }

        public static void InitializeBus(IEventHandlerFactory eventHandlerFactory)
        {
            Instance = new Bus(eventHandlerFactory);
        }
    }
}
