using System;
using PingerManager.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using PingerManager.Logging;
using System.Linq;

namespace PingerManager
{
    static class Program
    {
        private static ILogger _logger;

        static void Main()
        {
            #region Приветствие

            Console.WriteLine("Добро пожаловать в PingerManager!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nДля запуска работы нажмите Enter");
            Console.WriteLine("Для завершения работы нажмите Enter");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();

            #endregion

            var serviceProvider = PingerServiceProvider.ServiceProvider;
            var businessLogic = serviceProvider.GetService<IPingerBusinessLogic>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;

                var loggerProviders = serviceProvider.GetServices<ILoggerProvider>().ToList();
                loggerFactory.AddLoggerProvider(loggerProviders.First(o => o.GetType() == typeof(ConsoleLogger)));
                loggerFactory.AddLoggerProvider(loggerProviders.First(o => o.GetType() == typeof(TxtLogger)));
                _logger = loggerFactory.Logger;

                businessLogic.StartJob(token);

                Console.ReadLine();
                cts.Cancel();

                _logger?.Log(MessageType.Info, "Завершение работы ...");
            }
            catch (Exception e)
            {
                _logger?.Log(MessageType.Error, e.Message);
                throw;
            }
            finally
            {
                businessLogic.Dispose();
                serviceProvider.Dispose();
            }

            Console.WriteLine("До скорой встречи в PingerManager!");
            Console.ReadLine();
        }
    }
}
