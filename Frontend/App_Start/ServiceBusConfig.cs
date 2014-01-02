using System;
using System.Configuration;
using System.IO;
using MongoDataAccess;
using TaskScheduler;
using TaskScheduler.EventBus;
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
            eventHandler.RegisterInstance(() => new UpdateTaskResponseStatusEventHandler(mongoTaskRepository));
            eventHandler.RegisterInstance(() => new RunTaskEventHandler(new StandardDateTimeProvider(), mongoTaskRepository, new TimeSpanEvaluator(),
                new OperationResolver()));
            eventHandler.RegisterInstance(() => new ElapsedTimeEventHandler(mongoTaskRepository, new StandardDateTimeProvider()));
            eventHandler.RegisterInstance(() => new InitializeTaskEventHandler(new TimeSpanEvaluator(), new StandardDateTimeProvider(), mongoTaskRepository));
            eventHandler.RegisterInstance(() => new InitializeTaskManagerEventHandler(new JSonConfigurationRepository(config), mongoTaskRepository));
            Bus.InitializeBus(eventHandler);
        }
    }
}