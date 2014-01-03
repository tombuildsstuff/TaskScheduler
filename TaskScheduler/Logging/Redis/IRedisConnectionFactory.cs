namespace TaskScheduler.Logging.Redis
{
    public interface IRedisConnectionFactory
    {
        IRedisConnectionWrapper GetConnection();
    }
}