using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class Logger : ILogger
    {
        public List<ILoggerProvider> Providers { get; }

        public Logger()
        {
            Providers = new List<ILoggerProvider>();
        }

        public async Task LogAsync(LogParams logParams)
        {
            foreach (var provider in Providers)
            {
                await provider.LogAsync(logParams);
            }
        }

        public void Log(LogParams logParams)
        {
            foreach (var provider in Providers)
            {
                provider.LogAsync(logParams).GetAwaiter().GetResult();
            }
        }

        #region IDisposable
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Providers?.ForEach(p => p.Dispose());
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

    public enum MessageType
    {
        Info = 1,
        Warning,
        Error
    }
}
