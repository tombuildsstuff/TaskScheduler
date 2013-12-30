using System.Collections.Generic;

namespace TaskScheduler
{
    public interface IConfiguratinRepository
    {
        IEnumerable<TaskConfiguration> GetConfigurations();
    }
}