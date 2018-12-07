using PingerManager.Logging;
using System;
using System.Collections.Generic;

namespace PingerManager.Config
{
    public class ConfigReader : IConfigReader
    {
        private readonly IConfigStream _configStream;

        public ConfigReader(IConfigStream configStream)
        {
            _configStream = configStream;
        }

        public List<ConfigEntity> ReadConfig(ILogger logger)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            logger.Log(MessageType.Info, "Считывание конфигурации ...");
            Console.ForegroundColor = ConsoleColor.Gray;

            List<ConfigEntity> configEntityList = _configStream.ReadStream(logger);

            return configEntityList;
        }
    }
}
