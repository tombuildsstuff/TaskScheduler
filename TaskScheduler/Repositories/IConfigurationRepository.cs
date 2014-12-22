using System.Collections.Generic;

namespace TaskScheduler.Repositories
{
    public interface IConfigurationRepository
    {
        IEnumerable<TaskConfiguration> GetConfigurations();
    }
}