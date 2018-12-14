using System;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public async Task LogAsync(LogParams logParams)
        {
            Console.WriteLine(logParams.MessageType + ": " + logParams.Message);
            await Task.CompletedTask;
        }

        #region IDisposable
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing){}
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
