using Microsoft.Extensions.DependencyInjection;
using System;

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

        #region IDisposable
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Logger?.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
