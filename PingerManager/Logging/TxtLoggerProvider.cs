using System.Threading;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class TxtLoggerProvider : ILoggerProvider
    {
        private readonly ITxtLoggerWriter _txtLoggerWriter;
        private readonly ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();

        public TxtLoggerProvider(ITxtLoggerWriter txtLoggerWriter)
        {
            _txtLoggerWriter = txtLoggerWriter;
        }

        public async Task Log(LogParams logParams)
        {
            _readerWriterLock.EnterWriteLock();
            await _txtLoggerWriter.Write(logParams);
            _readerWriterLock.ExitWriteLock();
        }
    }
}
