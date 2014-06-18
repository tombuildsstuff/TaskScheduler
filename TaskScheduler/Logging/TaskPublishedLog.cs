using System;
using TaskScheduler.EventBus;

namespace TaskScheduler.Logging
{
    public class TaskPublishedLog<T> : LogStashLog where T : IEvent
    {
        public override string type
        {
            get { return "Task Scheduler - Task Published Event - V 1.0"; }
        }

        public T Event { get; set; }
        public Type TypeOfEvent { get; set; }
    }
}