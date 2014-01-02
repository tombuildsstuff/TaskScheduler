using System;
using System.Linq;
using System.Threading.Tasks;
using TaskScheduler.EventBus;
using TaskScheduler.Events;

namespace TaskScheduler.EventHandlers
{
    public class ElapsedTimeEventHandler : IEventHandler<ElapsedTimeEvent>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ElapsedTimeEventHandler(ITaskRepository taskRepository, IDateTimeProvider dateTimeProvider)
        {
            _taskRepository = taskRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public void Handle(ElapsedTimeEvent @event)
        {

            var enabledTasks = _taskRepository.GetAllTask().Where(x => x.Status == TaskStatus.Enabled);
            var now = _dateTimeProvider.NowUtc;
            Parallel.ForEach(enabledTasks, enabledTask =>
            {
                if (DateTime.Compare(enabledTask.NextRunningOn, now) == -1 && (enabledTask.NextRunningOn - now) <= new TimeSpan(0, 1, 0))
                {
                    Bus.Instance.Publish(new RunTaskEvent
                    {
                        Id = Guid.NewGuid(),
                        Frequency = enabledTask.Frequency,
                        LastRunningOn = enabledTask.LastRunningOn,
                        Status = enabledTask.Status,
                        ResponseStatus = enabledTask.ResponseStatus,
                        NextRunningOn = enabledTask.NextRunningOn,
                        TaskCommandParameters = enabledTask.TaskCommandParameters,
                        TaskCommandType = enabledTask.TaskCommandType,
                        Name = enabledTask.Name
                    });
                }
            });
        }
    }
}
