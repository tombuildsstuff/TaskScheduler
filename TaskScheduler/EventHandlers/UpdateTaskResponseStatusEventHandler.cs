using System;
using TaskScheduler.EventBus;
using TaskScheduler.Events;
using TaskScheduler.Repositories;

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
            if (task == null)
                return;
            task.UpdateResponseStatus(ConvertStringToStatus(@event.TaskStatus));
            _repository.SaveTaskInfo(task);
        }

        private static ResponseStatus ConvertStringToStatus(string taskStatus)
        {
            ResponseStatus status;

            if (Enum.TryParse(taskStatus, true, out status))
                return status;

            return ResponseStatus.Unknown;
        }
    }
}