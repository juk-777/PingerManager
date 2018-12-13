using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using PingerManager.Logging;

namespace PingerManager.Config
{
    public class AppSettingsStream : IConfigStream
    {
        private readonly IConfigurationBuilder _configurationBuilder;
        private readonly ILogger _logger;

        public AppSettingsStream(IConfigurationBuilder configurationBuilder, ILogger logger)
        {
            _configurationBuilder = configurationBuilder;
            _logger = logger;
        }
        public List<ConfigEntity> ReadStream()
        {
            var configEntityList = new List<ConfigEntity>();

            _configurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false);

            var configuration = _configurationBuilder.Build();

            var sectionMainLogPath = configuration.GetSection("MainLogPath");
            sectionMainLogPath.Get<MainLogPath>();
            
            var sectionConfigEntity = configuration.GetSection("ConfigEntities");
            var childSection = sectionConfigEntity.GetChildren();

            foreach (var child in childSection)
            {
                var configEntity = child.Get<ConfigEntity>();
                configEntityList.Add(configEntity);
            }

            _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Конфигурация считана успешно!", MainLogPath.LogPath)).GetAwaiter().GetResult();
            return configEntityList;
        }
    }
}
