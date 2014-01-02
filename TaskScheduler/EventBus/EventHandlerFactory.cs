using System;
using System.Collections.Generic;

namespace TaskScheduler.EventBus
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        public static IDictionary<Type, Func<object>> Container;

        static EventHandlerFactory()
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