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
            ILogger logger = null;

            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;

                var loggerProviders = serviceProvider.GetServices<ILoggerProvider>().ToList();
                loggerFactory.AddLoggerProvider(loggerProviders.First(o => o.GetType() == typeof(ConsoleLogger)));
                loggerFactory.AddLoggerProvider(loggerProviders.First(o => o.GetType() == typeof(TxtLogger)));
                logger = loggerFactory.CreateLogger();

                businessLogic.StartJob(token, logger);

                Console.ReadLine();
                cts.Cancel();

                logger.Log(MessageType.Info, "Завершение работы ...");
            }
            catch (Exception e)
            {
                logger?.Log(MessageType.Error, e.Message);
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
