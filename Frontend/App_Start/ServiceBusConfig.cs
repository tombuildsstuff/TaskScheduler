using System;
using System.Configuration;
using System.IO;
using System.Threading;
using MongoDataAccess;
using TaskScheduler;
using TaskScheduler.EventBus;
using TaskScheduler.EventHandlers;
using TaskScheduler.Events;
using TaskScheduler.Logging;
using TaskScheduler.Logging.Redis;
using TaskScheduler.Operations;

namespace Frontend
{
    public class ServiceBusConfig
    {
        public static void Initialize()
        {
            var eventHandler = new EventHandlerFactory();
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskSchedulerConfiguration.json");
            var config = File.OpenText(fileName).ReadToEnd();
            var mongourl = ConfigurationManager.AppSettings["MongoUrl"];
            var mongoTaskRepository = new MongoTaskRepository(mongourl);
            eventHandler.RegisterInstance(() => new UpdateTaskResponseStatusEventHandler(mongoTaskRepository));
            eventHandler.RegisterInstance(() => new RunTaskEventHandler(new StandardDateTimeProvider(), mongoTaskRepository, new TimeSpanEvaluator(),
                new OperationResolver()));
            eventHandler.RegisterInstance(() => new ElapsedTimeEventHandler(mongoTaskRepository, new StandardDateTimeProvider()));
            eventHandler.RegisterInstance(() => new InitializeTaskEventHandler(new TimeSpanEvaluator(), new StandardDateTimeProvider(), mongoTaskRepository));
            eventHandler.RegisterInstance(() => new InitializeTaskManagerEventHandler(new JSonConfigurationRepository(config), mongoTaskRepository));
            eventHandler.RegisterInstance(() => new ExceptionRaisedEventHandler(new RedisLogger<LogstashLog>(new RedisConnectionFactory(new RedisConnectionWrapper(), "10.10.20.68", 6379, 3))));
            Bus.InitializeBus(eventHandler);
        }
    }
}