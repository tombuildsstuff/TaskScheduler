namespace TaskScheduler
{
    public interface IErrorLogRepository
    {
        void Save(ErrorEntry entry);
    }
}