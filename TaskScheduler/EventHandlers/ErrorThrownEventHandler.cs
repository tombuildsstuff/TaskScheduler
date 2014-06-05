using System;
using TaskScheduler.EventBus;
using TaskScheduler.Events;

namespace TaskScheduler.EventHandlers
{
    public class ErrorThrownEventHandler : IEventHandler<ErrorThrownEvent>
    {
        private readonly IErrorLogRepository _errorLogRepository;

        public ErrorThrownEventHandler(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public void Handle(ErrorThrownEvent @event)
        {
            try
            {
                _errorLogRepository.Save(new ErrorEntry
                {
                    DateTime = DateTime.UtcNow,
                    ErrorMessage = @event.Exception.Message,
                    StackTrace = @event.Exception.StackTrace
                });
            }
            catch
            {
            }
        }
    }
}