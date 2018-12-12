using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class TxtLoggerProvider : ILoggerProvider
    {
        private readonly ITxtLoggerWriter _txtLoggerWriter;

        public TxtLoggerProvider(ITxtLoggerWriter txtLoggerWriter)
        {
            _txtLoggerWriter = txtLoggerWriter;
        }

        public async Task Log(LogParams logParams)
        {
            await _txtLoggerWriter.Write(logParams);
        }
    }
}
