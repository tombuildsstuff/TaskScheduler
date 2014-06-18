using System;
using System.Threading.Tasks;
using TaskScheduler.EventBus.EventStore;
using TaskScheduler.Events;
using TaskScheduler.Logging;

namespace TaskScheduler.EventBus
{
    public class Bus : IBus
    {
        private readonly IEventHandlerFactory _eventFactory;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IRedisLogger _redisLogger;

        public static IBus Instance { get; private set; }

        private Bus(IEventHandlerFactory eventFactory, IEventStoreRepository eventStoreRepository, IRedisLogger redisLogger)
        {
            _eventFactory = eventFactory;
            _eventStoreRepository = eventStoreRepository;
            _redisLogger = redisLogger;
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            if (_eventStoreRepository != null)
                _eventStoreRepository.PublishEvent(@event);
            _redisLogger.Log(new TaskPublishedLog<T>()
            {
                Message = "Event published",
                TypeOfEvent = typeof(T),
                Event = @event
            });
            Task.Factory.StartNew(() => _eventFactory.GetInstanceOf<T>().Handle(@event)).ContinueWith(t =>
            {
                if (t.IsFaulted) Publish(new ErrorThrownEvent
                {
                    Exception = t.Exception,
                    Id = Guid.NewGuid()
                });
            });

        }

        public static void InitializeBus(IEventHandlerFactory eventHandlerFactory, IEventStoreRepository repository, IRedisLogger redisLogger)
        {
            Instance = new Bus(eventHandlerFactory, repository, redisLogger);
        }
    }
}
