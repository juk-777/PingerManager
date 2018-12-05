using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
                if (string.IsNullOrEmpty(configEntity.Host))
                {
                    Console.WriteLine("Хост не задан!");
                    return false;
                }

                string urlPattern = @"([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                if (!Regex.IsMatch(configEntity.Host, urlPattern, RegexOptions.IgnoreCase))
                {
                    Console.WriteLine($"Хост {configEntity.Host} задан не корректно!");
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
