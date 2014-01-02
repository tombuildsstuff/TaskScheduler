using System.Collections.Generic;

namespace TaskScheduler.Services
{
    public interface ITaskMonitoringService
    {
        IEnumerable<TaskInfo> GetAllTasks();
        void UpdateTaskResponseStatus(string taskName, string responseStatus);
    }
}