using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskScheduler.EventBus
{
    public class Bus
    {
        private readonly IEventHandlerFactory _eventFactory;

        public Bus(IEventHandlerFactory eventFactory)
        {
            _eventFactory = eventFactory;
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            _eventFactory.GetInstanceOf<T>().Handle(@event);
        }
    }

    public interface IEvent
    {
        Guid Id { get; set; }
    }

    public interface IEventHandlerFactory
    {
        void RegisterInstance<T>(IEventHandler<T> instance) where T : IEvent;
        IEventHandler<T> GetInstanceOf<T>() where T : IEvent;
    }
    public interface IEventHandler<in T> where T : IEvent
    {
        void Handle(T @event);
    }
}
