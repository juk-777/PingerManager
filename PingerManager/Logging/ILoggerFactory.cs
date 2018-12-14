using System;

namespace PingerManager.Logging
{
    public interface ILoggerFactory : IDisposable
    {
        ILogger Logger { get; }
        void AddLoggerProvider(ILoggerProvider loggerProvider);
    }
}
