namespace TaskScheduler.Logging.Messages
{
    public abstract class LogStashLog
    {
        public string Name
        {
            get { return string.Format("TaskScheduler-{0}-{1}", Type, Version).ToLower(); }
        }

        public abstract string Message { get; }

        protected abstract string Type { get; }

        protected string Version
        {
            get { return "1"; }
        }
    }
}