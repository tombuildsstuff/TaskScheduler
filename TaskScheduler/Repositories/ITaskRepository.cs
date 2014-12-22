using System.Collections.Generic;
using TaskScheduler.Entities;

namespace TaskScheduler.Repositories
{
    public interface ITaskRepository
    {
        TaskInfo GetTaskByName(string name);
        IEnumerable<TaskInfo> GetAllTask();
        void SaveTaskInfo(TaskInfo task);
    }
}