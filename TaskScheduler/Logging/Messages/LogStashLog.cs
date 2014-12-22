namespace TaskScheduler.Logging.Messages
{
    public abstract class LogStashLog
    {
        public string Name
        {
            get { return string.Format("TaskScheduler-{0}-{1}", Type, Version).ToLower(); }
        }

        public string Message { get; set; }

        protected abstract string Type { get; }

        protected string Version
        {
            get { return "1"; }
        }
    }
}