using System;

namespace TaskScheduler.EventBus
{
    public interface IEvent
    {
        Guid Id { get; set; }
    }
}