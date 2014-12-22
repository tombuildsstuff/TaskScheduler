using System;
using System.Threading;

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

        public RedisConnectionFactory(IRedisConnectionWrapper connectionWrapper, string hostname, int port, int retryTime)
        {
            _connectionWrapper = connectionWrapper;
            _hostname = hostname;
            _port = port;
            _retryTime = retryTime;
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