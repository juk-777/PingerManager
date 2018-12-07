using System.Collections.Generic;

namespace PingerManager.Logging
{
    class LoggerFactory : ILoggerFactory
    {
        private readonly List<ILoggerProvider> _loggerProviders = new List<ILoggerProvider>();

        public void AddLoggerProvider(ILoggerProvider loggerProvider)
        {
            _loggerProviders.Add(loggerProvider);
        }

        public Logger CreateLogger()
        {
            var logger = new Logger
            {
                Providers = _loggerProviders
            };

            return logger;
        }
    }
}
