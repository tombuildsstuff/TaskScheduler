using MongoDB.Driver;
using TaskScheduler;
using TaskScheduler.Configuration;
using TaskScheduler.Entities;
using TaskScheduler.Repositories;

namespace MongoDataAccess
{
    public class MongoErrorLogRepository : IErrorLogRepository
    {
        private const string CollectionName = "ErrorLog";
        private const string DbName = "TaskScheduler";

        private readonly string _mongoUrl;

        public MongoErrorLogRepository(IConfiguration configuration)
        {
            _mongoUrl = configuration.Mongo.Address;
        }

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