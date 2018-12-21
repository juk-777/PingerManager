using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PingerManager.Logging;

namespace PingerManager.Config
{
    public class AppSettingsStream : IConfigStream
    {
        private readonly ILogger _logger;

        public AppSettingsStream(ILogger logger)
        {
            _logger = logger;
        }
        public List<ConfigEntity> ReadStream(IConfiguration configuration)
        {
            var sectionMainLogPath = configuration.GetSection("MainLogPath");
            sectionMainLogPath.Get<MainLogPath>();
            
            var sectionConfigEntity = configuration.GetSection("ConfigEntities");
            var childSection = sectionConfigEntity.GetChildren();

            var configEntityList = childSection.Select(child => child.Get<ConfigEntity>()).ToList();

            _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Конфигурация считана успешно!"));
            return configEntityList;
        }
    }
}
