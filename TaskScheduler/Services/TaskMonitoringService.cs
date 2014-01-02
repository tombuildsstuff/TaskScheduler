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

        public void UpdateTaskResponseStatus(string taskName, string taskStatus)
        {
            var task = _repository.GetTaskByName(taskName);
            if (task != null)
            {
                task.UpdateResponseStatus(ConvertStringToStatus(taskStatus));
                _repository.SaveTaskInfo(task);
            }
        }

        private static ResponseStatus ConvertStringToStatus(string taskStatus)
        {
            switch (taskStatus.ToLower())
            {
                case "started":
                    return ResponseStatus.Started;
                case "finished":
                    return ResponseStatus.Finished;
                    default:
                    return ResponseStatus.Unknown;
            }
        }
    }
}
