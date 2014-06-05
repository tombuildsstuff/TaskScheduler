using System;
using System.Threading.Tasks;
using TaskScheduler.EventBus.EventStore;
using TaskScheduler.Events;

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
            if (_eventStoreRepository != null)
                _eventStoreRepository.PublishEvent(@event);
                Task.Factory.StartNew(() => _eventFactory.GetInstanceOf<T>().Handle(@event)).ContinueWith(t =>
                {
                    if (t.IsFaulted) Publish(new ErrorThrownEvent
                    {
                        Exception = t.Exception,
                        Id = Guid.NewGuid()
                    });
                });
        }

        public static void InitializeBus(IEventHandlerFactory eventHandlerFactory, IEventStoreRepository repository)
        {
            Instance = new Bus(eventHandlerFactory, repository);
        }
    }
}
