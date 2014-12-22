using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TaskScheduler.Logging.Messages;

namespace TaskScheduler.Logging
{
    public interface IRedisLogger
    {
        void Log<T>(T log) where T : LogStashLog;
    }

    public class RedisLogger : IRedisLogger
    {
        private readonly IRedisConnectionFactory _connectionFactory;
        private readonly string _logstashDistributionList;

        public RedisLogger(IRedisConnectionFactory connectionFactory, string logstashDistributionList)
        {
            _connectionFactory = connectionFactory;
            _logstashDistributionList = logstashDistributionList;
        }

        public void Log<T>(T log) where T : LogStashLog
        {
            try
            {
                var settings = new JsonSerializerSettings { Converters = { new StringEnumConverter(), new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffZ" } } };
                PopulateLog(log);
                var content = JsonConvert.SerializeObject(log, settings);
                _connectionFactory.GetConnection().AddToList(_logstashDistributionList, content);
            }
            catch
            {
                //This is just log. Don't want it to affect request path ever.                
            }
        }

        private static void PopulateLog(LogStashLog log)
        {
            log.Host = Environment.MachineName;
            log.TimeStamp = DateTime.UtcNow;
        }
    }
}