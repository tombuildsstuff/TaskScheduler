using System;
using TaskScheduler.EventBus;
using TaskScheduler.Events;
using TaskScheduler.Logging;
using TaskScheduler.Logging.Messages;
using TaskScheduler.Operations;

namespace TaskScheduler.EventHandlers
{
    public class RunTaskEventHandler : IEventHandler<RunTaskEvent>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ITaskRepository _taskRepository;
        private readonly ITimeSpanEvaluator _timeSpanEvaluator;
        private readonly IOperationResolver _operationResolver;
        private readonly IRedisLogger _logger;

        public RunTaskEventHandler(IDateTimeProvider dateTimeProvider, ITaskRepository taskRepository, ITimeSpanEvaluator timeSpanEvaluator, IOperationResolver operationResolver, IRedisLogger logger)
        {
            _dateTimeProvider = dateTimeProvider;
            _taskRepository = taskRepository;
            _timeSpanEvaluator = timeSpanEvaluator;
            _operationResolver = operationResolver;
            _logger = logger;
        }

        public void Handle(RunTaskEvent @event)
        {
            var task = new TaskInfo(@event.Name, @event.Status, @event.LastRunningOn, @event.NextRunningOn, @event.TaskCommandType, @event.TaskCommandParameters, @event.Frequency, @event.ResponseStatus);
            task.UpdateLastRunningOn(_dateTimeProvider.NowUtc);
            task.UpdateNextRunningOn(_timeSpanEvaluator.Evaluate(_dateTimeProvider.NowUtc, task.Frequency));
            UpdateTaskWithStatus(task, ResponseStatus.Unknown);

            ResponseStatus? result = null;
            try
            {
                UpdateTaskWithStatus(task, ResponseStatus.Started);
                result = RunTask(task);
                UpdateTaskWithStatus(task, result.Value);
            }
            catch(Exception ex)
            {
                UpdateTaskWithStatus(task, result ?? ResponseStatus.Exception);
                Bus.Instance.Publish(new ErrorThrownEvent { Task = task, Exception = ex, Id = Guid.NewGuid()});
            }
        }

        private void UpdateTaskWithStatus(TaskInfo task, ResponseStatus status)
        {
            task.UpdateResponseStatus(status);
            _taskRepository.SaveTaskInfo(task);
            _logger.Log(new TaskStatusChangedLog(task.Name, status));
        }

        private ResponseStatus RunTask(TaskInfo info)
        {
            var operation = _operationResolver.Resolve(info.TaskCommandType);
            return operation.Execute(info.TaskCommandParameters);
        }
    }
}
