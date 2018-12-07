using PingerManager.Logging;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PingerManager.Config
{
    class ConfigVerifier : IConfigVerifier
    {
        private readonly ILoggerFactory _loggerFactory;

        public ConfigVerifier(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
        public bool Verify(List<ConfigEntity> configEntityList)
        {
            var logger = _loggerFactory.Logger;
            logger.Log(MessageType.Info, "Проверка конфигурации ...");

            foreach (ConfigEntity configEntity in configEntityList)
            {
                if (string.IsNullOrEmpty(configEntity.Host))
                {
                    logger.Log(MessageType.Error, "Хост не задан!");
                    return false;
                }

                string urlPattern = @"([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                if (!Regex.IsMatch(configEntity.Host, urlPattern, RegexOptions.IgnoreCase))
                {
                    logger.Log(MessageType.Error, $"Хост {configEntity.Host} задан не корректно!");
                    return false;
                }

                if (configEntity.Period <= 0)
                {
                    logger.Log(MessageType.Error, "Период задан неверно!");
                    return false;
                }

                if (string.IsNullOrEmpty(configEntity.Protocol))
                {
                    logger.Log(MessageType.Error, "Протокол не задан!");
                    return false;
                }

                if (configEntity.Protocol != "ICMP" && configEntity.Protocol != "HTTP" && configEntity.Protocol != "TCP")
                {
                    logger.Log(MessageType.Error, $"Протокол: { configEntity.Protocol} не поддерживается!");
                    return false;
                }

                if (configEntity.Protocol == "TCP")
                {
                    if (configEntity.Port < 0)
                    {
                        logger.Log(MessageType.Error, "Порт задан неверно!");
                        return false;
                    }
                }

                if (configEntity.Protocol == "HTTP")
                {
                    if (configEntity.ValidStatusCode < 0)
                    {
                        logger.Log(MessageType.Error, "Валидный статус код задан неверно!");
                        return false;
                    }
                }
            }

            logger.Log(MessageType.Info, "Проверка завершена успешно!");
            return true;
        }
    }
}
