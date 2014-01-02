using System;
using TaskScheduler.EventBus;

namespace TaskScheduler.Events
{
    public class ElapsedTimeEvent : IEvent
    {
        public Guid Id { get; set; }
    }
}
