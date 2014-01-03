using System.Threading.Tasks;

namespace TaskScheduler.Logging.Redis
{
    public interface IRedisConnectionWrapper
    {
        void OpenConnection(string hostname, int port);
        bool IsOpen();
        Task<long> AddToList(string listName, string content);
    }
}