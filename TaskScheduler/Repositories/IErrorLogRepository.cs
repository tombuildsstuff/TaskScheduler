namespace TaskScheduler.Repositories
{
    public interface IErrorLogRepository
    {
        void Save(ErrorEntry entry);
    }
}