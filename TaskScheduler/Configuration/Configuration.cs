using System;
using System.Configuration;
using System.IO;
using TaskScheduler.Configuration.Entities;

namespace TaskScheduler.Configuration
{
    public class Configuration : IConfiguration
    {
        public Configuration()
        {
            Logger = new LoggerSettings
            {
                Host = ConfigurationManager.AppSettings["RedisHost"],
                RetryTime = int.Parse(ConfigurationManager.AppSettings["RedisRetryTime"]),
                Port = int.Parse(ConfigurationManager.AppSettings["RedisPort"])
            };
            Mongo = new MongoSettings
            {
                Address = ConfigurationManager.AppSettings["MongoUrl"]
            };
            Tasks = new TaskSettings
            {
                FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskSchedulerConfiguration.json")
            };
        }

        public LoggerSettings Logger { get; private set; }

        public MongoSettings Mongo { get; private set; }

        public TaskSettings Tasks { get; private set; }
    }
}