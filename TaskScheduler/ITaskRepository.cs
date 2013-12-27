namespace TaskScheduler
{
    public interface ITaskRepository
    {
        TaskInfo GetTaskByName(string name);
        void SaveTaskInfo(TaskInfo task);
    }
}