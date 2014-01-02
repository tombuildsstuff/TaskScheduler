using System;

namespace TaskScheduler.EventBus
{
    public interface IEventHandlerFactory
    {
        void RegisterInstance<T>(Func<IEventHandler<T>> instance) where T : IEvent;
        IEventHandler<T> GetInstanceOf<T>() where T : IEvent;
    }
}