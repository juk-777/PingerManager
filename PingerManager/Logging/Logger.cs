using System.Collections.Generic;

namespace PingerManager.Logging
{
    public class Logger : ILogger
    {
        public List<ILoggerProvider> Providers { get; set; }

        public void Log(MessageType messageType, string message)
        {
            Providers.ForEach(p => p.Log(messageType, message));
        }
    }

    public enum MessageType
    {
        Info = 1,
        Warning,
        Error
    }
}
