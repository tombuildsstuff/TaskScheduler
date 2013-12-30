using System;
using System.Configuration;
using System.IO;
using MongoDataAccess;
using TaskScheduler;
using TaskScheduler.Operations;

namespace Frontend.App_Start
{
    public class TaskSchedulerConfig
    {
        public static void Start()
        {
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskSchedulerConfiguration.json");
            var config = File.OpenText(fileName).ReadToEnd();
            var mongourl = ConfigurationManager.AppSettings["MongoUrl"];
            var scheduler = new TaskManager(new JSonConfigurationRepository(config), new MongoTaskRepository(mongourl),
                new TimeSpanEvaluator(), new StandardDateTimeProvider(), new OperationResolver());
            scheduler.Start();
        }
    }
}