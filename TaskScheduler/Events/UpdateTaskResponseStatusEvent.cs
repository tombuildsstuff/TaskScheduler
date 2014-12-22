﻿using System;
using TaskScheduler.EventBus;

namespace TaskScheduler.Events
{
    public class UpdateTaskResponseStatusEvent : IEvent
    {
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public string TaskStatus { get; set; }
    }
}
