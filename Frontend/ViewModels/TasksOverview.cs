using System.Collections.Generic;
using System.Linq;
using TaskScheduler;

namespace Frontend.ViewModels
{
    public class TasksOverview
    {
        public TasksOverview (IEnumerable<TaskInfo> tasks, bool triggered)
        {
            Tasks = tasks;
            Triggered = triggered;
        }

        public IEnumerable<TaskInfo> Tasks { get; set; }

        public bool Triggered { get; set; }

        public IList<TaskInfo> DisabledTasks
        {
            get { return Tasks.Where(t => t.Status == TaskStatus.Disabled).ToList(); }
        }

        public IList<TaskInfo> EnabledTasks
        {
            get { return Tasks.Where(t => t.Status == TaskStatus.Enabled).ToList(); }
        }
    }
}