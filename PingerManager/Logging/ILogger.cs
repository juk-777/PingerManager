using System.Collections.Generic;

namespace PingerManager.Logging
{
    public interface ILogger
    {
        List<ILoggerProvider> Providers { get; }
        void Log(LogParams logParams);
    }
}
