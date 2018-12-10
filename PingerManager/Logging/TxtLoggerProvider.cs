namespace PingerManager.Logging
{
    public class TxtLoggerProvider : ILoggerProvider
    {
        private readonly ITxtLoggerWriter _txtLoggerWriter;
        private readonly object _locker = new object();

        public TxtLoggerProvider(ITxtLoggerWriter txtLoggerWriter)
        {
            _txtLoggerWriter = txtLoggerWriter;
        }

        public void Log(LogParams logParams)
        {
            lock (_locker)
            {
                _txtLoggerWriter.Write(logParams);
            }
        }
    }
}
