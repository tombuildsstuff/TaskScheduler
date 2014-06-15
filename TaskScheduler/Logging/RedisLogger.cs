using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TaskScheduler.Logging
{
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
                _connectionFactory.GetConnection()
                                  .AddToList(_logstashDistributionList,
                                             JsonConvert.SerializeObject(log,
                                                                         new JsonSerializerSettings
                                                                         {
                                                                             Converters = {new StringEnumConverter()}
                                                                         }));
            }
            catch
            {
                //This is just log. Don't want it to affect request path ever.                
            }
        }

    }
}