using System;
using TaskScheduler.EventBus;

namespace TaskScheduler.Events
{
    public class ExceptionRaisedEvent : IEvent
    {
        public Guid Id { get; set; }
        public Exception Exception { get; set; }
    }
}
