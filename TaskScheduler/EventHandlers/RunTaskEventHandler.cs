using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskScheduler.EventBus;
using TaskScheduler.Events;
using TaskScheduler.Operations;

namespace TaskScheduler.EventHandlers
{
    public class RunTaskEventHandler : IEventHandler<RunTaskEvent>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ITaskRepository _taskRepository;
        private readonly ITimeSpanEvaluator _timeSpanEvaluator;
        private readonly IOperationResolver _operationResolver;

        public RunTaskEventHandler(IDateTimeProvider dateTimeProvider, ITaskRepository taskRepository, ITimeSpanEvaluator timeSpanEvaluator, IOperationResolver operationResolver)
        {
            _dateTimeProvider = dateTimeProvider;
            _taskRepository = taskRepository;
            _timeSpanEvaluator = timeSpanEvaluator;
            _operationResolver = operationResolver;
        }

        public void Handle(RunTaskEvent @event)
        {
            var task = new TaskInfo(@event.Name, @event.Status, @event.LastRunningOn, @event.NextRunningOn,
                @event.TaskCommandType, @event.TaskCommandParameters, @event.Frequency, @event.ResponseStatus);
            task.UpdateLastRunningOn(_dateTimeProvider.NowUtc);
            task.UpdateNextRunningOn(_timeSpanEvaluator.Evaluate(_dateTimeProvider.NowUtc, task.Frequency));
            task.UpdateResponseStatus(ResponseStatus.Unknown);
            _taskRepository.SaveTaskInfo(task);
            try
            {
                RunTask(task);
            }
            catch
            {
                task.UpdateResponseStatus(ResponseStatus.ConnectionFailed);
                _taskRepository.SaveTaskInfo(task);
            }
        }
        private void RunTask(TaskInfo info)
        {
            var operation = _operationResolver.Resolve(info.TaskCommandType);
            operation.Execute(info.TaskCommandParameters);
        }
    }
}
