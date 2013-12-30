using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TaskScheduler;
using TaskStatus = TaskScheduler.TaskStatus;

namespace MongoDataAccess
{
    public class MongoTaskRepository : ITaskRepository
    {
        internal class InternalTaskInfo
        {
            public ObjectId _id { get; set; }
            public string Name { get; set; }
            public TaskStatus Status { get; set; }
            public DateTime LastRunningOn { get; set; }
            public DateTime NextRunningOn { get; set; }
            public string TaskCommandType { get; set; }
            public string TaskCommandParameters { get; set; }
            public string Frequency { get; set; }
        }

        private readonly string _mongoUrl;
        private const string CollectionName = "TaskInformations";
        private const string DbName = "TaskScheduler";

        public MongoTaskRepository(string mongoUrl)
        {
            _mongoUrl = mongoUrl;
        }

        public TaskInfo GetTaskByName(string name)
        {
            var collection = GetTaskCollection();
            var info = collection.FindOne(Query<TaskInfo>.EQ(x => x.Name, name));
            return info == null ? null : TaskInfoMapper(info);
        }

        public IEnumerable<TaskInfo> GetAllTask()
        {
            var collection = GetTaskCollection().FindAll();
            return collection.Select(TaskInfoMapper);
        }

        public void SaveTaskInfo(TaskInfo task)
        {
            var collection = GetTaskCollection();
            var info = collection.FindOne(Query<TaskInfo>.EQ(x => x.Name, task.Name));
            if (info == null)
            {
                collection.Insert(new InternalTaskInfo
                {
                    Name = task.Name,
                    Frequency = task.Frequency,
                    LastRunningOn = task.LastRunningOn,
                    NextRunningOn = task.NextRunningOn,
                    Status = task.Status,
                    TaskCommandParameters = task.TaskCommandParameters,
                    TaskCommandType = task.TaskCommandType
                });
            }
            else
            {
                collection.Save(new InternalTaskInfo()
                {
                    _id = info._id,
                    Status = task.Status,
                    Frequency = task.Frequency,
                    LastRunningOn = task.LastRunningOn,
                    NextRunningOn = task.NextRunningOn,
                    Name = task.Name,
                    TaskCommandParameters = task.TaskCommandParameters,
                    TaskCommandType = task.TaskCommandType
                });
            }
        }

        private static TaskInfo TaskInfoMapper(InternalTaskInfo info)
        {
            return new TaskInfo(info.Name, info.Status, info.LastRunningOn, info.NextRunningOn,
                info.TaskCommandType, info.TaskCommandParameters, info.Frequency);
        }

        private MongoCollection<InternalTaskInfo> GetTaskCollection()
        {
            var mongoClient = new MongoClient(_mongoUrl);
            var server = mongoClient.GetServer();
            var db = server.GetDatabase(DbName);
            var collection = db.GetCollection<InternalTaskInfo>(CollectionName);
            return collection;
        }
    }
}
