using System.Collections.Generic;

namespace TaskScheduler
{
    public interface ITaskRepository
    {
        TaskInfo GetTaskByName(string name);
        IEnumerable<TaskInfo> GetAllTask();
        void SaveTaskInfo(TaskInfo task);
    }
}