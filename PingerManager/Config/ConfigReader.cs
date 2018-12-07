using PingerManager.Logging;
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
            logger.Log(MessageType.Info, "Считывание конфигурации ...");

            List<ConfigEntity> configEntityList = _configStream.ReadStream(logger);

            return configEntityList;
        }
    }
}
