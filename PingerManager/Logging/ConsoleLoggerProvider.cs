using System;

namespace PingerManager.Logging
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public void Log(MessageType messageType, string message)
        {
            Console.WriteLine(messageType + ": " + message);
        }
    }
}
