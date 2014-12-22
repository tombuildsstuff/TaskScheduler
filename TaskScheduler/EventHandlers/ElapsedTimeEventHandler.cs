using System;
using System.Linq;
using System.Threading.Tasks;
using TaskScheduler.EventBus;
using TaskScheduler.Events;
using TaskScheduler.Repositories;
using TaskScheduler.Services.Dates;
using TaskStatus = TaskScheduler.Entities.TaskStatus;

namespace TaskScheduler.EventHandlers
{
    public class ElapsedTimeEventHandler : IEventHandler<ElapsedTimeEvent>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IDateService _dateService;

        public ElapsedTimeEventHandler(ITaskRepository taskRepository, IDateService dateService)
        {
            _taskRepository = taskRepository;
            _dateService = dateService;
        }

        public void Handle(ElapsedTimeEvent @event)
        {

            var enabledTasks = _taskRepository.GetAllTask().Where(x => x.Status == TaskStatus.Enabled);
            var now = _dateService.NowUtc;
            Parallel.ForEach(enabledTasks, enabledTask =>
            {
                if (DateTime.Compare(enabledTask.NextRunningOn, now) == -1 && (enabledTask.NextRunningOn - now) <= new TimeSpan(0, 1, 0))
                {
                    Bus.Instance.Publish(RunTaskEvent.FromTask(enabledTask));
                }
            });
        }
    }
}
