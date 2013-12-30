using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaskScheduler
{
    public class JSonConfigurationRepository : IConfiguratinRepository
    {
        private readonly string _textConfiguration;

        public JSonConfigurationRepository(string textConfiguration)
        {
            _textConfiguration = textConfiguration;
        }

        public IEnumerable<TaskConfiguration> GetConfigurations()
        {
            return JsonConvert.DeserializeObject<IEnumerable<TaskConfiguration>>(_textConfiguration);
        }
    }
}