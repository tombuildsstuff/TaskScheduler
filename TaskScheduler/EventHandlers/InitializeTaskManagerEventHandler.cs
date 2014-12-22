using System;
using System.Collections.Generic;
using TaskScheduler.Entities;
using TaskScheduler.EventBus;
using TaskScheduler.Events;
using TaskScheduler.Repositories;

namespace TaskScheduler.EventHandlers
{
    public class InitializeTaskManagerEventHandler : IEventHandler<InitializeTaskManagerEvent>
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ITaskRepository _taskRepository;

        public InitializeTaskManagerEventHandler(IConfigurationRepository configurationRepository, ITaskRepository taskRepository)
        {
            _configurationRepository = configurationRepository;
            _taskRepository = taskRepository;
        }

        public void Handle(InitializeTaskManagerEvent @event)
        {
            var configurations = _configurationRepository.GetConfigurations();
            PreserveTaskHistory();
            ReconfigureTaskStatus(configurations);
        }
        private static void ReconfigureTaskStatus(IEnumerable<TaskConfiguration> configurations)
        {
            foreach (var cfg in configurations)
            {
                Bus.Instance.Publish(new InitializeTaskEvent
                {
                    Id = Guid.NewGuid(),
                    CommandParameters = cfg.CommandParameters,
                    CommandType = cfg.CommandType,
                    Frequency = cfg.Frequency,
                    Name = cfg.Name
                });
            }
        }

        private void PreserveTaskHistory()
        {
            var tasks = _taskRepository.GetAllTask();
            foreach (var taskInfo in tasks)
            {
                taskInfo.Disable();
                _taskRepository.SaveTaskInfo(taskInfo);
            }
        }
    }
}
