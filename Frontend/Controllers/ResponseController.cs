using System.IO;
using System.Web.Mvc;

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