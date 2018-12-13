using System.IO;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class TxtLoggerWriter : ITxtLoggerWriter
    {
        public async Task Write(LogParams logParams)
        {
            var writePath = Path.Combine(logParams.LogPath);

            using (var textWriter = TextWriter.Synchronized(new StreamWriter(writePath, true)))
            {
                await textWriter.WriteLineAsync(logParams.MessageType + ": " + logParams.Message);
                await textWriter.FlushAsync();
            }
        }
    }
}
