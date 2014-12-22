namespace TaskScheduler.Configuration.Entities
{
    public class LoggerSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public int RetryTime { get; set; }
    }
}