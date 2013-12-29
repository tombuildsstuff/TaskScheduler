using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace TaskScheduler
{
    public class TaskManager : ITaskManager
    {
        private readonly IConfiguratinRepository _configuratinRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ITimeSpanEvaluator _timeSpanEvaluator;
        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly Timer _timer;

        public TaskManager(IConfiguratinRepository configuratinRepository, ITaskRepository taskRepository, ITimeSpanEvaluator timeSpanEvaluator, IDateTimeProvider dateTimeProvider)
        {
            _configuratinRepository = configuratinRepository;
            _taskRepository = taskRepository;
            _timeSpanEvaluator = timeSpanEvaluator;
            _dateTimeProvider = dateTimeProvider;

            _timer = new Timer(60000)
            {
                AutoReset = true
            };
            _timer.Elapsed += ElapsedInterval;
        }

        private void ElapsedInterval(object sender, ElapsedEventArgs e)
        {
            var enabledTasks = _taskRepository.GetAllTask().Where(x => x.Status == TaskStatus.Enabled);
            foreach (var enabledTask in enabledTasks)
            {
                if ((enabledTask.NextRunningOn - _dateTimeProvider.NowUtc) <= new TimeSpan(0, 1, 0))
                {
                    runTask(enabledTask);
                }
            }
        }

        private void runTask(TaskInfo info)
        {
            // resolve command type
            // create instance with parameters
            //if success update next execution time
        }

        private void InitializeTasks()
        {
            var configurations = _configuratinRepository.GetConfigurations();
            DisableAllTasks();
            UpdateTaskByConfiguration(configurations);
        }

        private void UpdateTaskByConfiguration(IEnumerable<TaskConfiguration> configurations)
        {
            foreach (var cfg in configurations)
            {
                var task = _taskRepository.GetTaskByName(cfg.Name) ?? GenerateTaskFromConfiguration(cfg);
                var nextTimeRunning = EvaluateNextRunningTime(cfg.Frequency);
                task.Enable();
                task.UpdateNextRunningOn(nextTimeRunning);
                _taskRepository.SaveTaskInfo(task);
            }
        }

        private DateTime EvaluateNextRunningTime(string frequency)
        {
            return _timeSpanEvaluator.Evaluate(_dateTimeProvider.NowUtc, frequency);
        }

        private TaskInfo GenerateTaskFromConfiguration(TaskConfiguration cfg)
        {
            return new TaskInfo(cfg.Name, TaskStatus.Enabled, DateTime.MinValue, new DateTime(), cfg.CommandType, cfg.CommandParameters );
        }

        private void DisableAllTasks()
        {
            var tasks = _taskRepository.GetAllTask();
            foreach (var taskInfo in tasks)
            {
                taskInfo.Disable();
                _taskRepository.SaveTaskInfo(taskInfo);
            }
        }

        public void Start()
        {
            InitializeTasks();
            _timer.Start();
        }
    }
}