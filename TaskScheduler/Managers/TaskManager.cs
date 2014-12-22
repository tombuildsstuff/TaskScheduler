using System;
using System.Timers;
using TaskScheduler.EventBus;
using TaskScheduler.Events;

namespace TaskScheduler.Managers
{
    public class TaskManager : ITaskManager
    {
        private readonly Timer _timer;

        public TaskManager()
        {
            _timer = new Timer(60000)
            {
                AutoReset = true
            };
            _timer.Elapsed += ElapsedInterval;
        }

        public void Start()
        {
            Bus.Instance.Publish(new InitializeTaskManagerEvent { Id = Guid.NewGuid()});
            _timer.Start();
        }

        private void ElapsedInterval(object sender, ElapsedEventArgs e)
        {
            Bus.Instance.Publish(new ElapsedTimeEvent { Id = Guid.NewGuid()});
        }
    }
}