namespace TaskScheduler.EventBus
{
    public class Bus : IBus
    {
        private readonly IEventHandlerFactory _eventFactory;
        public IBus Instace { get; private set; }

        private Bus(IEventHandlerFactory eventFactory)
        {
            _eventFactory = eventFactory;
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            _eventFactory.GetInstanceOf<T>().Handle(@event);
        }

        public void InitializeBus(IEventHandlerFactory eventHandlerFactory)
        {
            Instace = new Bus(eventHandlerFactory);
        }
    }
}
