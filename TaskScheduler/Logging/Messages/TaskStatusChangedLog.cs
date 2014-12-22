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

        protected override string Type
        {
            get { return "TaskStatusChangedLog"; }
        }
    }
}