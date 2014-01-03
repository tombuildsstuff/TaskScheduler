using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskScheduler.EventBus;
using TaskScheduler.Events;
using TaskScheduler.Logging;

namespace TaskScheduler.EventHandlers
{
    public class ExceptionRaisedEventHandler : IEventHandler<ExceptionRaisedEvent>
    {
        private readonly IRedisLogger<LogstashLog> _redisLogger;

        public ExceptionRaisedEventHandler(IRedisLogger<LogstashLog> redisLogger)
        {
            _redisLogger = redisLogger;
        }

        public void Handle(ExceptionRaisedEvent @event)
        {
            _redisLogger.Log(new LogstashLog
            {
                Exception = @event.Exception,
                Message = @event.Exception.Message,
                Name = "ExceptionRaisedEvent",
                Version = "0.1"
            });
        }
    }
}
