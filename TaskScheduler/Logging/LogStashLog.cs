namespace TaskScheduler.Logging
{
    public abstract class LogStashLog
    {
        public string Name
        {
            get { return "Task Scheduler Event Logging"; }
        }

        public string Message { get; set; }
        public abstract string type { get; }
        public string Version
        {
            get { return "1.0"; }
        }
    }
}