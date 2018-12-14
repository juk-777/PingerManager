using PingerManager.Config;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class TxtLoggerProvider : ILoggerProvider
    {
        private readonly ITxtLoggerWriter _txtLoggerWriter;
        private readonly TextWriter _writer;

        public TxtLoggerProvider(ITxtLoggerWriter txtLoggerWriter)
        {
            _txtLoggerWriter = txtLoggerWriter;
            _writer = TextWriter.Synchronized(new StreamWriter(MainLogPath.LogPath ?? "log_main.txt", true));
        }

        public async Task LogAsync(LogParams logParams)
        {
            await _txtLoggerWriter.WriteAsync(logParams, _writer);
        }

        #region IDisposable
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _writer?.Dispose();
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
