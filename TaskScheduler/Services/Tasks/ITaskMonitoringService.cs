using System.Collections.Generic;
using TaskScheduler.Entities;

namespace TaskScheduler.Services.Tasks
{
    public interface ITaskMonitoringService
    {
        IEnumerable<TaskInfo> GetAllTasks();
        
        TaskInfo GetByName(string taskName);

        void UpdateTaskResponseStatus(string taskName, string responseStatus);
    }
}