using System;
using TaskScheduler.EventBus;

namespace TaskScheduler.Events
{
    public class InitializeTaskEvent : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CommandType { get; set; }
        public string CommandParameters { get; set; }
        public string Frequency { get; set; }
    }
}
