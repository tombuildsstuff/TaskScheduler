using System.Net;
using System.Web.Mvc;
using Frontend.ViewModels;
using MongoDataAccess;
using TaskScheduler.Configuration;
using TaskScheduler.EventBus;
using TaskScheduler.Events;
using TaskScheduler.Services;
using TaskScheduler.Services.Tasks;

namespace Frontend.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskMonitoringService _taskMonitoringService;

        public TasksController()
        {
            _taskMonitoringService = new TaskMonitoringService(new MongoTaskRepository(new Configuration()));
        }

        public ActionResult Index(bool triggered = false)
        {
            var tasks = _taskMonitoringService.GetAllTasks();
            var model = new TasksOverview(tasks, triggered);
            return View(model);
        }

        [HttpPost]
        public ActionResult Trigger(string taskName)
        {
            var task = _taskMonitoringService.GetByName(taskName);
            if (task == null) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Task not found!");
            }

            Bus.Instance.Publish(RunTaskEvent.FromTask(task));
            return RedirectToAction("index", "tasks", new { triggered = true });
        }
    }
}