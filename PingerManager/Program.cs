using System;
using PingerManager.BusinessLogic;
using PingerManager.Config;
using Microsoft.Extensions.DependencyInjection;

namespace PingerManager
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Добро пожаловать в PingerManager!");

            var serviceProvider = new ServiceCollection()
                .AddTransient<IPingerBusinessLogic, PingerBusinessLogic>()
                .AddTransient<IConfigReader, ConfigReader>()
                .AddTransient<IConfigStream, AppSettingsStream>()
                .BuildServiceProvider();

            var businessLogic = serviceProvider.GetService<IPingerBusinessLogic>();

            try
            {
                businessLogic.StartJob();
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


            Console.WriteLine("\nДо скорой встречи в PingerManager ...");
            Console.ReadLine();

        }
    }
}
