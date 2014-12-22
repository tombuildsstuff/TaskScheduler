using System;
using System.Threading;
using TaskScheduler.Configuration.Entities;

namespace TaskScheduler.Logging
{
    public interface IRedisConnectionFactory
    {
        IRedisConnectionWrapper GetConnection();
    }

    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly IRedisConnectionWrapper _connectionWrapper;
        private readonly string _hostname;
        private readonly int _port;
        private readonly int _retryTime;
        private readonly Object _connectionInitLock = new object();

        public RedisConnectionFactory(IRedisConnectionWrapper connectionWrapper, LoggerSettings settings)
        {
            _connectionWrapper = connectionWrapper;
            _hostname = settings.Host;
            _port = settings.Port;
            _retryTime = settings.RetryTime;
            InitializeConnection();
        }

        public IRedisConnectionWrapper GetConnection()
        {
            if (!_connectionWrapper.IsOpen())
                TryInitializeConnection();

            return _connectionWrapper;
        }

        private void TryInitializeConnection()
        {
            if (!Monitor.TryEnter(_connectionInitLock))
                return;

            try
            {
                Thread.Sleep(_retryTime);
                InitializeConnection();
            }
            finally
            {
                Monitor.Exit(_connectionInitLock);
            }
        }

        private void InitializeConnection()
        {
            _connectionWrapper.OpenConnection(_hostname, _port);
        }
    }
}