using System.Collections.Generic;

namespace TaskScheduler.Services
{
    public class TaskMonitoringService : ITaskMonitoringService
    {
        private readonly ITaskRepository _repository;

        public TaskMonitoringService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskInfo> GetAllTasks()
        {
            return _repository.GetAllTask();
        }
    }
}
