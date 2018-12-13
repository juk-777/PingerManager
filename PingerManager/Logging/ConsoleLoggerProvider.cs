using System;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public async Task Log(LogParams logParams)
        {
            Console.WriteLine(logParams.MessageType + ": " + logParams.Message);
            await Task.CompletedTask;
        }
    }
}
