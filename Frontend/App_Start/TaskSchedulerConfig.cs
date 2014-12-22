using System;
using System.Configuration;
using System.IO;
using TaskScheduler;
using TaskScheduler.Managers;
using TaskScheduler.Operations;

namespace Frontend.App_Start
{
    public class TaskSchedulerConfig
    {
        public static void Start()
        {
            var scheduler = new TaskManager();
            scheduler.Start();
        }
    }
}