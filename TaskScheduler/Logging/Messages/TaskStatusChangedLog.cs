using TaskScheduler.Entities;

namespace TaskScheduler.Logging.Messages
{
    public class TaskStatusChangedLog : LogStashLog
    {
        public TaskStatusChangedLog(string taskName, ResponseStatus status)
        {
            TaskName = taskName;
            Status = status;
        }

        public ResponseStatus Status { get; set; }

        public string TaskName { get; set; }

        public override string Message
        {
            get { return string.Format("{0} updated it's status to {1}", TaskName, Status); }
        }

        protected override string Type
        {
            get { return "TaskStatusChangedLog"; }
        }
    }
}