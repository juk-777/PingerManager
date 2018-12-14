using PingerManager.Config;
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
            _writer = TextWriter.Synchronized(new StreamWriter(MainLogPath.LogPath ?? "log_main.txt"));
        }

        public async Task Log(LogParams logParams)
        {
            await _txtLoggerWriter.Write(logParams, _writer);
        }
    }
}
