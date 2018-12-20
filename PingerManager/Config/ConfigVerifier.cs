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
            _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Проверка конфигурации ..."));

            foreach (var configEntity in configEntityList)
            {
                if (string.IsNullOrEmpty(configEntity.Host))
                {
                    _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + "Хост не задан!"));
                    return false;
                }

                const string urlPattern = @"([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                if (!Regex.IsMatch(configEntity.Host, urlPattern, RegexOptions.IgnoreCase))
                {
                    _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + $"Хост {configEntity.Host} задан не корректно!"));
                    return false;
                }

                if (configEntity.Period <= 0)
                {
                    _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + "Период задан неверно!"));
                    return false;
                }

                if (configEntity.Protocol != Protocol.Icmp && configEntity.Protocol != Protocol.Http && configEntity.Protocol != Protocol.Tcp)
                {
                    _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + $"Протокол: { configEntity.Protocol} не поддерживается!"));
                    return false;
                }

                switch (configEntity.Protocol)
                {
                    case Protocol.Tcp when configEntity.Port < 0:
                        _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + "Порт задан неверно!"));
                        return false;
                    case Protocol.Http when configEntity.ValidStatusCode < 0:
                        _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + "Валидный статус код задан неверно!"));
                        return false;
                    case Protocol.Icmp:
                        break;
                    default:
                        continue;
                }
            }

            _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Проверка завершена успешно!"));
            return true;
        }
    }
}
