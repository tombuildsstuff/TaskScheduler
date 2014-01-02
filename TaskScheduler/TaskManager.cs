using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using TaskScheduler.Operations;

namespace TaskScheduler
{
    public class TaskManager : ITaskManager
    {
        private readonly IConfiguratinRepository _configuratinRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ITimeSpanEvaluator _timeSpanEvaluator;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IOperationResolver _operationResolver;
        private readonly Timer _timer;

        public TaskManager(IConfiguratinRepository configuratinRepository, ITaskRepository taskRepository, ITimeSpanEvaluator timeSpanEvaluator, IDateTimeProvider dateTimeProvider, IOperationResolver operationResolver)
        {
            _configuratinRepository = configuratinRepository;
            _taskRepository = taskRepository;
            _timeSpanEvaluator = timeSpanEvaluator;
            _dateTimeProvider = dateTimeProvider;
            _operationResolver = operationResolver;

            _timer = new Timer(60000)
            {
                AutoReset = true
            };
            _timer.Elapsed += ElapsedInterval;
        }

        private void ElapsedInterval(object sender, ElapsedEventArgs e)
        {
            var enabledTasks = _taskRepository.GetAllTask().Where(x => x.Status == TaskStatus.Enabled);
            var now = _dateTimeProvider.NowUtc;
            Parallel.ForEach(enabledTasks, enabledTask =>
            {
                if (DateTime.Compare(enabledTask.NextRunningOn,now) == -1 && (enabledTask.NextRunningOn - now) <= new TimeSpan(0, 1, 0))
                {
                    enabledTask.UpdateLastRunningOn(_dateTimeProvider.NowUtc);
                    enabledTask.UpdateNextRunningOn(EvaluateNextRunningTime(enabledTask.Frequency));
                    enabledTask.UpdateResponseStatus(ResponseStatus.Unknown);
                    _taskRepository.SaveTaskInfo(enabledTask);
                    RunTask(enabledTask);
                }
            });
        }

        private void RunTask(TaskInfo info)
        {

            var operation = _operationResolver.Resolve(info.TaskCommandType);
            operation.Execute(info.TaskCommandParameters);
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
                var configurationTask = GenerateTaskFromConfiguration(cfg);
                var task = _taskRepository.GetTaskByName(cfg.Name);
                if (task != null)
                {
                    configurationTask.UpdateLastRunningOn(task.LastRunningOn);
                    configurationTask.UpdateResponseStatus(task.ResponseStatus);
                }
                var nextTimeRunning = EvaluateNextRunningTime(cfg.Frequency);
                configurationTask.UpdateNextRunningOn(nextTimeRunning);
                _taskRepository.SaveTaskInfo(configurationTask);
              
            }
        }

        private DateTime EvaluateNextRunningTime(string frequency)
        {
            return _timeSpanEvaluator.Evaluate(_dateTimeProvider.NowUtc, frequency);
        }

        private TaskInfo GenerateTaskFromConfiguration(TaskConfiguration cfg)
        {
            return new TaskInfo(cfg.Name, TaskStatus.Enabled, DateTime.MinValue, EvaluateNextRunningTime(cfg.Frequency), cfg.CommandType, cfg.CommandParameters, cfg.Frequency, ResponseStatus.Unknown);
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