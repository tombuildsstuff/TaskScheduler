using System.Configuration;
using System.IO;
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


    public class LoadBalancerStatusController : ApiController
    {
        public string Get()
        {
            try
            {
                using (
                    var file = new FileStream(@"/etc/lbstatus/taskscheduler", FileMode.Open,
                        FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(file))
                {
                    return reader.ReadToEnd();
                };
            }
            catch (IOException)
            {
                return "OTWEB_OFF";
            }
        }
    }
}