using System.Threading.Tasks;
using BookSleeve;

namespace TaskScheduler.Logging.Redis
{
    public class RedisConnectionWrapper : IRedisConnectionWrapper
    {
        private RedisConnection _connection = null;

        public void OpenConnection(string hostname, int port)
        {
            try
            {
                _connection = new RedisConnection(hostname, port);
                _connection.Open();
            }
            catch
            {
                _connection = null;
            }
        }

        public bool IsOpen()
        {
            return _connection != null && _connection.State == RedisConnectionBase.ConnectionState.Open;
        }

        public Task<long> AddToList(string listName, string content)
        {
            return _connection.Lists.AddLast(0, listName, content);
        }
    }
}
