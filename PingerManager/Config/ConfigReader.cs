using PingerManager.Logging;
using System.Collections.Generic;

namespace PingerManager.Config
{
    public class ConfigReader : IConfigReader
    {
        private readonly IConfigStream _configStream;
        private readonly ILoggerFactory _loggerFactory;

        public ConfigReader(IConfigStream configStream, ILoggerFactory loggerFactory)
        {
            _configStream = configStream;
            _loggerFactory = loggerFactory;
        }

        public List<ConfigEntity> ReadConfig()
        {
            var logger = _loggerFactory.Logger;
            logger.Log(MessageType.Info, "Считывание конфигурации ...");

            List<ConfigEntity> configEntityList = _configStream.ReadStream(logger);

            return configEntityList;
        }
    }
}
