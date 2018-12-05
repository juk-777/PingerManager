using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PingerManager.Config
{
    class AppSettingsStream : IConfigStream
    {
        public List<ConfigEntity> ReadStream()
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

            Console.WriteLine("Конфигурация считана!");
            return configEntityList;
        }
    }
}
