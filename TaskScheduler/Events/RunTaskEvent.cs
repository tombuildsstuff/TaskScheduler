using System;
using TaskScheduler.EventBus;

namespace TaskScheduler.Events
{
    public class RunTaskEvent : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime LastRunningOn { get; set; }
        public DateTime NextRunningOn { get; set; }
        public string TaskCommandType { get; set; }
        public string TaskCommandParameters { get; set; }
        public string Frequency { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
