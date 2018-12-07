using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using PingerManager.Logging;

namespace PingerManager.Config
{
    class AppSettingsStream : IConfigStream
    {
        public List<ConfigEntity> ReadStream(ILogger logger)
        {
            List<ConfigEntity> configEntityList = new List<ConfigEntity>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false);

            var configuration = builder.Build();
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
