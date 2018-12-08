using Microsoft.Extensions.DependencyInjection;

namespace PingerManager.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        public ILogger Logger { get; private set; }
        private readonly ServiceProvider _serviceProvider;

        public LoggerFactory()
        {
            _serviceProvider = PingerServiceProvider.ServiceProvider;
        }

        public void AddLoggerProvider(ILoggerProvider loggerProvider)
        {
            if (Logger == null)
            {
                Logger = _serviceProvider.GetService<ILogger>();
            }
            Logger.Providers.Add(loggerProvider);
        }
    }
}
