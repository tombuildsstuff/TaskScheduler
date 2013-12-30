using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler;

namespace MongoDataAccess
{
    public class MongoTaskRepository : ITaskRepository
    {
        private readonly string _mongoUrl;

        public MongoTaskRepository(string mongoUrl)
        {
            _mongoUrl = mongoUrl;
        }

        public TaskInfo GetTaskByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskInfo> GetAllTask()
        {
            throw new NotImplementedException();
        }

        public void SaveTaskInfo(TaskInfo task)
        {
            throw new NotImplementedException();
        }
    }
}
