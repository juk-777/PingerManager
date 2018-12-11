using System.Collections.Generic;

namespace PingerManager.Logging
{
    public class Logger : ILogger
    {
        public List<ILoggerProvider> Providers { get; }

        public Logger()
        {
            Providers = new List<ILoggerProvider>();
        }

        public void Log(LogParams logParams)
        {
            Providers.ForEach(p => p.Log(logParams));
        }
    }

    public enum MessageType
    {
        Info = 1,
        Error
    }
}
