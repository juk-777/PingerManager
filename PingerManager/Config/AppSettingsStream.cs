using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using PingerManager.Logging;

namespace PingerManager.Config
{
    class AppSettingsStream : IConfigStream
    {
        private readonly IConfigurationBuilder _configurationBuilder;
        private readonly ILoggerFactory _loggerFactory;

        public AppSettingsStream(IConfigurationBuilder configurationBuilder, ILoggerFactory loggerFactory)
        {
            _configurationBuilder = configurationBuilder;
            _loggerFactory = loggerFactory;
        }
        public List<ConfigEntity> ReadStream()
        {
            var logger = _loggerFactory.Logger;
            List<ConfigEntity> configEntityList = new List<ConfigEntity>();

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

            logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Конфигурация считана успешно!", MainLogPath.LogPath));
            return configEntityList;
        }
    }
}
