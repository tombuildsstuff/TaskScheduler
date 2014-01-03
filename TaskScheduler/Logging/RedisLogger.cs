using System;
using Newtonsoft.Json;
using TaskScheduler.Logging.Redis;

namespace TaskScheduler.Logging
{
    public interface IRedisLogger<T>
    {
        void Log(T log);
    }

    public class RedisLogger<T> : IRedisLogger<T> where T : ILogstashLog
    {
        private readonly IRedisConnectionFactory _connectionFactory;

        public RedisLogger(IRedisConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Log(T log)
        {
            try
            {
                var logEvent = new LogEvent<T>
                {
                    Log = log,
                    message = log.Message,
                    type = GetTypeString(log)
                };
                _connectionFactory.GetConnection().AddToList("logstash", JsonConvert.SerializeObject(logEvent));
            }
            catch
            {
                //This is just log. Don't want it to affect request path ever.                
            }
        }

        private string GetTypeString(T log)
        {
            return string.Format("taskscheduler.{0}.v{1}", log.Name, log.Version);
        }

        internal class LogEvent<TV>
        {
            public TV Log { get; set; }

            public string type { get; set; }

            public string message { get; set; }
        }

    }


    public interface ILogstashLog
    {
        string Name { get; }
        string Message { get; }
        string Version { get; }
    }

    public class LogstashLog : ILogstashLog
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string Version { get; set; }
        public Exception Exception { get; set; }
    }
}
