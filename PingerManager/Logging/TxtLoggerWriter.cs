using System.IO;
using System.Text;

namespace PingerManager.Logging
{
    public class TxtLoggerWriter : ITxtLoggerWriter
    {
        public async void Write(LogParams logParams)
        {
            string writePath = Path.Combine(logParams.LogPath);
            var strLog = new StringBuilder();
            strLog.Append(logParams.MessageType + ": " + logParams.Message);
            
            using (var sw = new StreamWriter(writePath, true, Encoding.Default))
            {
                await sw.WriteLineAsync(strLog.ToString().Trim());
            }
        }
    }
}
