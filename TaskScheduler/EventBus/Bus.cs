using System.Threading.Tasks;
using TaskScheduler.EventBus.EventStore;

namespace TaskScheduler.EventBus
{
    public class Bus : IBus
    {
        private readonly IEventHandlerFactory _eventFactory;
        private readonly IEventStoreRepository _eventStoreRepository;

        public static IBus Instance { get; private set; }

        private Bus(IEventHandlerFactory eventFactory, IEventStoreRepository eventStoreRepository)
        {
            _eventFactory = eventFactory;
            _eventStoreRepository = eventStoreRepository;
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            _eventStoreRepository.PublishEvent(@event);
            Task.Factory.StartNew(() => _eventFactory.GetInstanceOf<T>().Handle(@event));

        }

        public static void InitializeBus(IEventHandlerFactory eventHandlerFactory, IEventStoreRepository repository)
        {
            Instance = new Bus(eventHandlerFactory, repository);
        }
    }
}
