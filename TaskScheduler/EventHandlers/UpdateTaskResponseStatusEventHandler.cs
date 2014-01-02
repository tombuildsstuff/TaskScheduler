using TaskScheduler.EventBus;
using TaskScheduler.Events;

namespace TaskScheduler.EventHandlers
{
    public class UpdateTaskResponseStatusEventHandler : IEventHandler<UpdateTaskResponseStatusEvent>
    {
        private readonly ITaskRepository _repository;

        public UpdateTaskResponseStatusEventHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public void Handle(UpdateTaskResponseStatusEvent @event)
        {
            var task = _repository.GetTaskByName(@event.TaskName);
            if (task == null) return;
            task.UpdateResponseStatus(ConvertStringToStatus(@event.TaskStatus));
            _repository.SaveTaskInfo(task);
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
