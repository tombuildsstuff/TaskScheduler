using System.Configuration;
using System.Web.Http;
using MongoDataAccess;
using TaskScheduler.Services;

namespace Frontend.Controllers
{
    public class ResponseController : ApiController
    {
        private readonly ITaskMonitoringService _taskMonitoringService;

        public ResponseController()
        {
            _taskMonitoringService = new TaskMonitoringService(new MongoTaskRepository(ConfigurationManager.AppSettings["MongoUrl"]));
        }

        public void Post([FromUri] string taskName, [FromUri] string taskStatus)
        {
            _taskMonitoringService.UpdateTaskResponseStatus(taskName, taskStatus);
        }
    }
}