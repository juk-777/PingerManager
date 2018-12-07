using System;

namespace PingerManager.Logging
{
    public class ConsoleLogger : ILoggerProvider
    {
        private readonly object _locker = new object();

        public void Log(MessageType messageType, string message)
        {
            lock (_locker)
            {
                Console.WriteLine(messageType + ": " + message);
            }
        }
    }
}
