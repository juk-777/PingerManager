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
            var section = configuration.GetSection("ConfigEntities");
            var childSection = section.GetChildren();

            foreach (var child in childSection)
            {
                var configEntity = child.Get<ConfigEntity>();
                configEntityList.Add(configEntity);
            }

            logger.Log(MessageType.Info, "Конфигурация считана!");
            return configEntityList;
        }
    }
}
