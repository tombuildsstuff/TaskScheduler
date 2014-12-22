using System.Collections.Generic;
using TaskScheduler.Entities;

namespace TaskScheduler.Repositories
{
    public interface IConfigurationRepository
    {
        IEnumerable<TaskConfiguration> GetConfigurations();
    }
}