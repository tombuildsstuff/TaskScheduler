using System.Collections.Generic;
using Newtonsoft.Json;
using TaskScheduler.Configuration;
using TaskScheduler.Services.Files;

namespace TaskScheduler.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;

        public ConfigurationRepository(IConfiguration configuration, IFileService fileService)
        {
            _configuration = configuration;
            _fileService = fileService;
        }

        public IEnumerable<TaskConfiguration> GetConfigurations()
        {
            var filePath = _configuration.Tasks.FilePath;
            var textConfiguration = _fileService.GetContents(filePath);
            return JsonConvert.DeserializeObject<IEnumerable<TaskConfiguration>>(textConfiguration);
        }
    }
}