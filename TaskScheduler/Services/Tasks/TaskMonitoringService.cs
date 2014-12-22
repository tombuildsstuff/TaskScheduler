using System;
using System.Collections.Generic;
using TaskScheduler.Entities;
using TaskScheduler.EventBus;
using TaskScheduler.Events;
using TaskScheduler.Repositories;

namespace TaskScheduler.Services.Tasks
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

        public TaskInfo GetByName(string taskName)
        {
            return _repository.GetTaskByName(taskName);
        }

        public void UpdateTaskResponseStatus(string taskName, string taskStatus)
        {
            Bus.Instance.Publish(new UpdateTaskResponseStatusEvent
            {
                Id = Guid.NewGuid(),
                TaskName = taskName,
                TaskStatus = taskStatus
            });
        }
    }
}
