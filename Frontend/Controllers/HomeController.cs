using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDataAccess;
using TaskScheduler.Services;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskMonitoringService _taskMonitoringService;

        public HomeController()
        {
            _taskMonitoringService = new TaskMonitoringService(new MongoTaskRepository(ConfigurationManager.AppSettings["MongoUrl"]));
        }

        public ActionResult Index()
        {
            return View(_taskMonitoringService.GetAllTasks());
        }

     
    }
}