using System;
using System.Collections.Generic;

namespace PingerManager.Config
{
    class ConfigVerifier : IConfigVerifier
    {
        public bool Verify(List<ConfigEntity> configEntityList)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nПроверка конфигурации ...");
            Console.ForegroundColor = ConsoleColor.Gray;

            foreach (ConfigEntity configEntity in configEntityList)
            {
                if (string.IsNullOrEmpty(configEntity.Host) || !Uri.IsWellFormedUriString(configEntity.Host, UriKind.RelativeOrAbsolute))
                {
                    Console.WriteLine("Хост не задан или задан не корректно!");
                    return false;
                }

                if (configEntity.Period <= 0)
                {
                    Console.WriteLine("Период задан неверно!");
                    return false;
                }

                if (string.IsNullOrEmpty(configEntity.Protocol))
                {
                    Console.WriteLine("Протокол не задан!");
                    return false;
                }

                if (configEntity.Protocol != "ICMP" && configEntity.Protocol != "HTTP" && configEntity.Protocol != "TCP")
                {
                    Console.WriteLine($"Протокол: {configEntity.Protocol} не поддерживается!");
                    return false;
                }

                if (configEntity.Protocol == "TCP")
                {
                    if (configEntity.Port < 0)
                    {
                        Console.WriteLine("Порт задан неверно!");
                        return false;
                    }
                }

                if (configEntity.Protocol == "HTTP")
                {
                    if (configEntity.ValidStatusCode < 0)
                    {
                        Console.WriteLine("Валидный статус код задан неверно!");
                        return false;
                    }
                }
            }

            Console.WriteLine("Проверка завершена успешно!");
            return true;
        }
    }
}
