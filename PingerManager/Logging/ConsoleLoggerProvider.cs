using System;

namespace PingerManager.Logging
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public void Log(LogParams logParams)
        {
            Console.WriteLine(logParams.MessageType + ": " + logParams.Message);
        }
    }
}
