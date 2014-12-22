using System;
using TaskScheduler.EventBus;

namespace TaskScheduler.Logging.Messages
{
    public class TaskPublishedLog<T> : LogStashLog where T : IEvent
    {
        public T Event { get; set; }

        public Type TypeOfEvent { get; set; }

        public override string Message
        {
            get { return "Event published"; }
        }

        protected override string Type
        {
            get { return "TaskPublishedLog"; }
        }
    }
}