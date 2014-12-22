using MongoDataAccess;
using TaskScheduler.Configuration;
using TaskScheduler.EventBus;
using TaskScheduler.EventHandlers;
using TaskScheduler.Logging;
using TaskScheduler.Operations;
using TaskScheduler.Repositories;
using TaskScheduler.Services;
using TaskScheduler.Services.Dates;
using TaskScheduler.Services.Files;

namespace Frontend
{
    public class ServiceBusConfig
    {
        public static void Initialize()
        {
            var configuration = new Configuration();
            var eventHandler = new EventHandlerFactory();
            var mongoTaskRepository = new MongoTaskRepository(configuration);
            var logger = GetLogger(configuration);

            eventHandler.RegisterInstance(() => new UpdateTaskResponseStatusEventHandler(mongoTaskRepository));
            eventHandler.RegisterInstance(() => new RunTaskEventHandler(new DateService(), mongoTaskRepository, new OperationResolver(), logger));
            eventHandler.RegisterInstance(() => new ElapsedTimeEventHandler(mongoTaskRepository, new DateService()));
            eventHandler.RegisterInstance(() => new InitializeTaskEventHandler(new DateService(), mongoTaskRepository));
            eventHandler.RegisterInstance(() => new InitializeTaskManagerEventHandler(new ConfigurationRepository(configuration, new FileService()), mongoTaskRepository));
            eventHandler.RegisterInstance(() => new ErrorThrownEventHandler(new MongoErrorLogRepository(configuration)));
            Bus.InitializeBus(eventHandler, logger);
        }

        private static IRedisLogger GetLogger(IConfiguration configuration)
        {
            var logger = new RedisLogger(new RedisConnectionFactory(new RedisConnectionWrapper(), configuration.Logger), "logstash");
            return logger;
        }
    }
}