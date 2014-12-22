using System;

namespace TaskScheduler.Entities
{
    public class TaskInfo
    {
        public TaskInfo(string name, TaskStatus status, DateTime lastRunningOn, DateTime nextRunningOn, string taskCommandType, string taskCommandParameters, string frequency, ResponseStatus responseStatus)
        {
            ResponseStatus = responseStatus;
            Frequency = frequency;
            TaskCommandParameters = taskCommandParameters;
            TaskCommandType = taskCommandType;
            NextRunningOn = nextRunningOn;
            LastRunningOn = lastRunningOn;
            Status = status;
            Name = name;
        }

        public string Name { get; private set; }
        public TaskStatus Status { get; private set; }
        public DateTime LastRunningOn { get; private set; }
        public DateTime NextRunningOn { get; private set; }
        public string TaskCommandType { get; private set; }
        public string TaskCommandParameters { get; private set; }
        public string Frequency { get; private set; }
        public ResponseStatus ResponseStatus { get; private set; }

        public void UpdateLastRunningOn(DateTime lastRunningOn)
        {
            LastRunningOn = lastRunningOn;
        }

        public void UpdateNextRunningOn(DateTime nextRunningOn)
        {
            NextRunningOn = nextRunningOn;
        }

        public void Disable()
        {
            Status = TaskStatus.Disabled;
        }

        public void Enable()
        {
            Status = TaskStatus.Enabled;
        }

        public void UpdateFrequency(string frequency)
        {
            Frequency = frequency;
        }

        public void UpdateResponseStatus(ResponseStatus responseStatus)
        {
            ResponseStatus = responseStatus;
        }
    }
}