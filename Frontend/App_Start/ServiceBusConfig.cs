using System;
using System.Configuration;
using System.IO;
using MongoDataAccess;
using TaskScheduler;
using TaskScheduler.EventBus;
using TaskScheduler.EventBus.EventStore;
using TaskScheduler.EventHandlers;
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
            var eventStoreIp = ConfigurationManager.AppSettings["EventStoreIp"];
            var eventStorePort = int.Parse(ConfigurationManager.AppSettings["EventStoreTcpPort"]);
            var eventStoreUserName = ConfigurationManager.AppSettings["EventStoreUserName"];
            var eventStorePassword = ConfigurationManager.AppSettings["EventStorePassword"];
            var useEventStore = bool.Parse(ConfigurationManager.AppSettings["UseEventStore"]);
            var redisIp = ConfigurationManager.AppSettings["RedisIp"];
            var redisPort = int.Parse(ConfigurationManager.AppSettings["RedisPort"]);
            var redisMaxRetries = int.Parse(ConfigurationManager.AppSettings["RedisMaxRetries"]);
            eventHandler.RegisterInstance(() => new UpdateTaskResponseStatusEventHandler(mongoTaskRepository));
            eventHandler.RegisterInstance(() => new RunTaskEventHandler(new StandardDateTimeProvider(), mongoTaskRepository, new TimeSpanEvaluator(),
                new OperationResolver()));
            eventHandler.RegisterInstance(() => new ElapsedTimeEventHandler(mongoTaskRepository, new StandardDateTimeProvider()));
            eventHandler.RegisterInstance(() => new InitializeTaskEventHandler(new TimeSpanEvaluator(), new StandardDateTimeProvider(), mongoTaskRepository));
            eventHandler.RegisterInstance(() => new InitializeTaskManagerEventHandler(new JSonConfigurationRepository(config), mongoTaskRepository));
            eventHandler.RegisterInstance(() => new ErrorThrownEventHandler(new MongoErrorLogRepository(mongourl)));
            Bus.InitializeBus(eventHandler, useEventStore 
                ? new EventStoreRepository(new EventStoreConfiguration(eventStoreIp,eventStorePort,eventStoreUserName,eventStorePassword))
                : null);
        }
    }
}