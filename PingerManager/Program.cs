using System;
using PingerManager.BusinessLogic;
using PingerManager.Config;

namespace PingerManager
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Добро пожаловать в PingerManager!");
            var businessLogic = new PingerBusinessLogic(new ConfigReader(new AppSettingsStream()));

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
            }


            Console.WriteLine("\nДо скорой встречи в PingerManager ...");
            Console.ReadLine();

        }
    }
}
