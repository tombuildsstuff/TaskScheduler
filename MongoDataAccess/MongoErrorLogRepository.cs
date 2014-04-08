using MongoDB.Driver;
using TaskScheduler;

namespace MongoDataAccess
{
    public class MongoErrorLogRepository : IErrorLogRepository
    {
        private readonly string _mongoUrl;

        public MongoErrorLogRepository(string mongoUrl)
        {
            _mongoUrl = mongoUrl;
        }

        private const string CollectionName = "ErrorLog";
        private const string DbName = "TaskScheduler";

        public void Save(ErrorEntry errorEntry)
        {
            GetErrorCollection().Insert(errorEntry);
        }


        private MongoCollection<ErrorEntry> GetErrorCollection()
        {
            var mongoClient = new MongoClient(_mongoUrl);
            var server = mongoClient.GetServer();
            var db = server.GetDatabase(DbName);
            var collection = db.GetCollection<ErrorEntry>(CollectionName);
            return collection;
        }
    }
}