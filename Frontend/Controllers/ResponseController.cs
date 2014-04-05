using System.Configuration;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using MongoDataAccess;
using TaskScheduler.Services;

namespace Frontend.Controllers
{
    public class LoadBalancerStatusController : Controller
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