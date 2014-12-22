using System;
using TaskScheduler.Entities;
using TaskScheduler.EventBus;
using TaskScheduler.Events;
using TaskScheduler.Repositories;
using TaskScheduler.Services.Dates;

namespace TaskScheduler.EventHandlers
{
    public class InitializeTaskEventHandler : IEventHandler<InitializeTaskEvent>
    {
        private readonly IDateService _dateService;
        private readonly ITaskRepository _taskRepository;

        public InitializeTaskEventHandler(IDateService dateService, ITaskRepository taskRepository)
        {
            _dateService = dateService;
            _taskRepository = taskRepository;
        }

        public void Handle(InitializeTaskEvent @event)
        {
            var configurationTask = GenerateTaskFromEvent(@event);
            var task = _taskRepository.GetTaskByName(@event.Name);
            if (task != null)
            {
                configurationTask.UpdateLastRunningOn(task.LastRunningOn);
                configurationTask.UpdateResponseStatus(task.ResponseStatus);
            }

            var nextTimeRunning = EvaluateNextRunningTime(@event.Frequency);
            configurationTask.UpdateNextRunningOn(nextTimeRunning);
            _taskRepository.SaveTaskInfo(configurationTask);
        }

        private TaskInfo GenerateTaskFromEvent(InitializeTaskEvent cfg)
        {
            return new TaskInfo(cfg.Name, TaskStatus.Enabled, DateTime.MinValue, EvaluateNextRunningTime(cfg.Frequency), cfg.CommandType, cfg.CommandParameters, cfg.Frequency, ResponseStatus.Unknown);
        }

        private DateTime EvaluateNextRunningTime(string frequency)
        {
            return _dateService.Evaluate(_dateService.NowUtc, frequency);
        }
    }
}