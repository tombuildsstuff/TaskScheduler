using TaskScheduler.Configuration.Entities;

namespace TaskScheduler.Configuration
{
    public interface IConfiguration
    {
        LoggerSettings Logger { get; }

        MongoSettings Mongo { get; }

        TaskSettings Tasks { get; }
    }
}