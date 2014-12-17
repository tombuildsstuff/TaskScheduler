using System.Collections.Generic;

namespace TaskScheduler.Services
{
    public interface ITaskMonitoringService
    {
        IEnumerable<TaskInfo> GetAllTasks();
        
        TaskInfo GetByName(string taskName);

        void UpdateTaskResponseStatus(string taskName, string responseStatus);
    }
}