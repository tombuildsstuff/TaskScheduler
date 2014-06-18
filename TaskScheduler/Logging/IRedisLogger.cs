namespace TaskScheduler.Logging
{
    public interface IRedisLogger
    {
        void Log<T>(T log) where T : LogStashLog;
    }
}