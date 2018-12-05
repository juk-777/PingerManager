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

        public List<ConfigEntity> ReadConfig()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Считывание конфигурации ...");
            Console.ForegroundColor = ConsoleColor.Gray;

            List<ConfigEntity> configEntityList = _configStream.ReadStream();

            foreach (ConfigEntity configEntity in configEntityList)
            {
                Console.WriteLine($"\nHost: {configEntity.Host} \nPeriod: {configEntity.Period} \nProtocol: {configEntity.Protocol} \nPort: {configEntity.Port} \nValidStatusCode: {configEntity.ValidStatusCode}");
            }

            return configEntityList;
        }
    }
}
