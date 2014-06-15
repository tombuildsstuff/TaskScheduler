namespace TaskScheduler.Logging
{
    public abstract class LogStashLog
    {
        public string Name
        {
            get { return "Task Scheduler Logging"; }
        }

        public string Message { get; set; }
        public abstract string Type { get; }
        public string Version
        {
            get { return "1.0"; }
        }
    }
}