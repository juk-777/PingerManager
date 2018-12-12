using System;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public async Task Log(LogParams logParams)
        {
            await Task.Run(() => Console.WriteLine(logParams.MessageType + ": " + logParams.Message));
        }
    }
}
