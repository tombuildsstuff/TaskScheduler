using System;
using System.Collections.Generic;

namespace TaskScheduler
{
    public interface ITaskRepository
    {
        TaskInfo GetTaskByName(string name);
        void SaveTaskInfo(TaskInfo task);
    }

    public interface IConfiguratinRepository
    {
        IEnumerable<TaskConfiguration> GetConfigurations();
    }

    public interface ITaskManager
    {
        void InitializeTasks();
        void Start();
    }

    public class TaskManager : ITaskManager
    {
        private readonly IConfiguratinRepository _configuratinRepository;
        private readonly ITaskRepository _taskRepository;

        public TaskManager(IConfiguratinRepository configuratinRepository, ITaskRepository taskRepository)
        {
            _configuratinRepository = configuratinRepository;
            _taskRepository = taskRepository;
        }

        public void InitializeTasks()
        {
            var configurations = _configuratinRepository.GetConfigurations();
            DisableAllTasks();
            UpdateTaskByConfiguration(configurations);
        }

        private void UpdateTaskByConfiguration(IEnumerable<TaskConfiguration> configurations)
        {
            foreach (var cfg in configurations)
            {
                var task = _taskRepository.GetTaskByName(cfg.Name) ?? GenerateTaskFromConfiguration(cfg);
            }
        }

        private TaskInfo GenerateTaskFromConfiguration(TaskConfiguration cfg)
        {
            return new TaskInfo(cfg.Name, TaskStatus.Enabled, DateTime.MinValue, new DateTime(), cfg.CommandType, cfg.CommandParameters );
        }

        private void DisableAllTasks()
        {
           
        }

        public void Start()
        {
            
        }
    }
}