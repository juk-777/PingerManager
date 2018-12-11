using PingerManager.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PingerManager.Config
{
    public class ConfigVerifier : IConfigVerifier
    {
        private readonly ILogger _logger;

        public ConfigVerifier(ILogger logger)
        {
            _logger = logger;
        }
        public bool Verify(IEnumerable<ConfigEntity> configEntityList)
        {
            _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Проверка конфигурации ...", MainLogPath.LogPath));

            foreach (var configEntity in configEntityList)
            {
                if (string.IsNullOrEmpty(configEntity.Host))
                {
                    _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + "Хост не задан!", MainLogPath.LogPath));
                    return false;
                }

                string urlPattern = @"([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                if (!Regex.IsMatch(configEntity.Host, urlPattern, RegexOptions.IgnoreCase))
                {
                    _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + $"Хост {configEntity.Host} задан не корректно!", MainLogPath.LogPath));
                    return false;
                }

                if (configEntity.Period <= 0)
                {
                    _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + "Период задан неверно!", MainLogPath.LogPath));
                    return false;
                }

                if (string.IsNullOrEmpty(configEntity.Protocol))
                {
                    _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + "Протокол не задан!", MainLogPath.LogPath));
                    return false;
                }

                if (configEntity.Protocol != "ICMP" && configEntity.Protocol != "HTTP" && configEntity.Protocol != "TCP")
                {
                    _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + $"Протокол: { configEntity.Protocol} не поддерживается!", MainLogPath.LogPath));
                    return false;
                }

                if (configEntity.Protocol == "TCP")
                {
                    if (configEntity.Port < 0)
                    {
                        _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + "Порт задан неверно!", MainLogPath.LogPath));
                        return false;
                    }
                }

                if (configEntity.Protocol == "HTTP")
                {
                    if (configEntity.ValidStatusCode < 0)
                    {
                        _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + "Валидный статус код задан неверно!", MainLogPath.LogPath));
                        return false;
                    }
                }
            }

            _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Проверка завершена успешно!", MainLogPath.LogPath));
            return true;
        }
    }
}
