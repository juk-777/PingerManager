﻿using System;
using PingerManager.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using PingerManager.Logging;
using System.Linq;

namespace PingerManager
{
    internal static class Program
    {
        private static ILogger _logger;

        private static void Main()
        {
            #region Hello

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Добро пожаловать в PingerManager!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nДля запуска работы нажмите <Enter>");
            Console.WriteLine("Для отмены - нажмите <C>");
            Console.WriteLine("Для завершения работы нажмите <Enter>");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();

            #endregion

            var serviceProvider = PingerServiceProvider.ServiceProvider;
            var businessLogic = serviceProvider.GetService<IPingerBusinessLogic>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            try
            {
                var loggerProviders = serviceProvider.GetServices<ILoggerProvider>().ToList();
                loggerFactory.AddLoggerProvider(loggerProviders.First(o => o.GetType() == typeof(ConsoleLoggerProvider)));
                loggerFactory.AddLoggerProvider(loggerProviders.First(o => o.GetType() == typeof(TxtLoggerProvider)));
                _logger = loggerFactory.Logger;

                businessLogic.StartJob(token);

                var ch = Console.ReadKey(true).KeyChar;
                if (ch == 'c' || ch == 'C' || ch == 'с' || ch == 'С')
                {
                    cts.Cancel();
                    _logger?.LogAsync(new LogParams(MessageType.Info, DateTime.Now + " " + "Получен запрос на отмену операции ...")).GetAwaiter().GetResult();
                }

                if (ch != '\r')
                {
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                }

                _logger?.LogAsync(new LogParams(MessageType.Info, DateTime.Now + " " + "Завершение работы ...")).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                _logger?.LogAsync(new LogParams(MessageType.Error, e.Message)).GetAwaiter().GetResult();
                throw;
            }
            finally
            {
                businessLogic.Dispose();
                cts.Dispose();
                serviceProvider.Dispose();
                _logger?.Dispose();
            }

            #region Goodbye

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("До скорой встречи в PingerManager!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();

            #endregion
        }
    }
}
