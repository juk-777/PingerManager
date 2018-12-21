using Microsoft.Extensions.Configuration;
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

        public List<ConfigEntity> ReadConfig(IConfiguration configuration)
        {
            var configEntityList = _configStream.ReadStream(configuration);

            return configEntityList;
        }
    }
}
