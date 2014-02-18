using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage Get()
        {
            try
            {
                using (
                    var file = new FileStream(@"/etc/lbstatus/taskscheduler", FileMode.Open,
                        FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(file))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(reader.ReadToEnd())
                    };
                }
            }
            catch (IOException)
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("OTWEB_OFF")
                };
            }
        }
    }
}