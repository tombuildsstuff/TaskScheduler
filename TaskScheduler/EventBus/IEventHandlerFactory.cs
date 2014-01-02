using System;
using System.Collections.Generic;

namespace TaskScheduler.EventBus
{
    public interface IEventHandlerFactory
    {
        void RegisterInstance<T>(Func<IEventHandler<T>> instance) where T : IEvent;
        IEventHandler<T> GetInstanceOf<T>() where T : IEvent;
    }

    public class InMemoryEventHandlerFactory : IEventHandlerFactory
    {
        public static IDictionary<Type, Func<object>> Container;

        static InMemoryEventHandlerFactory()
        {
            Container = new Dictionary<Type, Func<object>>();
        }
        public void RegisterInstance<T>(Func<IEventHandler<T>> instance) where T : IEvent
        {
            if (!Container.ContainsKey(typeof(T)))
            {
                Container.Add(typeof(T), instance);
            }
        }

        public IEventHandler<T> GetInstanceOf<T>() where T : IEvent
        {
            if (Container.ContainsKey(typeof(T)))
            {
                return (IEventHandler<T>)Container[typeof(T)]();
            }

            return null;
        }
    }
}