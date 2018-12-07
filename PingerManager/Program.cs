using System;
using PingerManager.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace PingerManager
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Добро пожаловать в PingerManager!");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nДля запуска работы нажмите Enter");
            Console.WriteLine("Для завершения работы нажмите Enter");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();

            var serviceProvider = PingerServiceProvider.ServiceProvider;
            var businessLogic = serviceProvider.GetService<IPingerBusinessLogic>();

            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;

                businessLogic.StartJob(token);

                Console.ReadLine();
                cts.Cancel();

                Console.WriteLine("\nЗавершение работы ...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
