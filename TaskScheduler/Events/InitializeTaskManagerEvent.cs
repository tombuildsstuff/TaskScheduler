using System;
using TaskScheduler.EventBus;

namespace TaskScheduler.Events
{
    public class InitializeTaskManagerEvent : IEvent
    {
        public Guid Id { get; set; }
    }
}
