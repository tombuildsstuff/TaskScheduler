namespace TaskScheduler.Logging
{
    public interface IRedisConnectionFactory
    {
        IRedisConnectionWrapper GetConnection();
    }
}