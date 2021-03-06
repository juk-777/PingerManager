﻿using System;
using PingerManager.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using PingerManager.Logging;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;

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
            var configurationBuilder = serviceProvider.GetService<IConfigurationBuilder>();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false);
            var configuration = configurationBuilder.Build();
            var businessLogic = serviceProvider.GetService<IPingerBusinessLogic>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            try
            {
                var loggerProviders = serviceProvider.GetServices<ILoggerProvider>().ToList();
                foreach (var provider in loggerProviders)
                {
                    loggerFactory.AddLoggerProvider(provider);
                }
                _logger = loggerFactory.Logger;

                businessLogic.StartJob(configuration, token);

                var ch = Console.ReadKey(true).KeyChar;
                if (ch == 'c' || ch == 'C' || ch == 'с' || ch == 'С')
                {
                    cts.Cancel();
                    _logger?.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Получен запрос на отмену операции ..."));
                    businessLogic.Dispose();
                }

                if (ch != '\r')
                {
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                }

                _logger?.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Завершение работы ..."));
            }
            catch (Exception e)
            {
                _logger?.Log(new LogParams(MessageType.Error, e.Message));
                throw;
            }
            finally
            {
                cts.Dispose();
                businessLogic?.Dispose();
                Thread.Sleep(2000);
                loggerFactory.Dispose();
                serviceProvider.Dispose();
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
