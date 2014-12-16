using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskScheduler.EventBus;
using TaskScheduler.Events;

namespace TaskScheduler.EventHandlers
{
    public class InitializeTaskEventHandler : IEventHandler<InitializeTaskEvent>
    {
        private readonly ITimeSpanEvaluator _timeSpanEvaluator;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ITaskRepository _taskRepository;

        public InitializeTaskEventHandler(ITimeSpanEvaluator timeSpanEvaluator, IDateTimeProvider dateTimeProvider, ITaskRepository taskRepository)
        {
            _timeSpanEvaluator = timeSpanEvaluator;
            _dateTimeProvider = dateTimeProvider;
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
            return new TaskInfo(cfg.Name, TaskStatus.Enabled, DateTime.MinValue, EvaluateNextRunningTime(cfg.Frequency), cfg.CommandType, cfg.CommandParameters, cfg.Frequency, OperationResponse.Unknown);
        }

        private DateTime EvaluateNextRunningTime(string frequency)
        {
            return _timeSpanEvaluator.Evaluate(_dateTimeProvider.NowUtc, frequency);
        }
    }
}
