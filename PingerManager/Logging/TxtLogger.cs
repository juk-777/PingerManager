namespace PingerManager.Logging
{
    public class TxtLogger : ILoggerProvider
    {
        private readonly ITxtLoggerWriter _txtLoggerWriter;
        private readonly object _locker = new object();

        public TxtLogger(ITxtLoggerWriter txtLoggerWriter)
        {
            _txtLoggerWriter = txtLoggerWriter;
        }

        public void Log(MessageType messageType, string message)
        {
            lock (_locker)
            {
                _txtLoggerWriter.Write(messageType, message);
            }
        }
    }
}
