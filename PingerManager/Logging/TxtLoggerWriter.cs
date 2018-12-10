using System.IO;
using System.Text;

namespace PingerManager.Logging
{
    public class TxtLoggerWriter : ITxtLoggerWriter
    {
        public void Write(LogParams logParams)
        {
            string writePath = Path.Combine(logParams.LogPath);
            StringBuilder strLog = new StringBuilder();
            strLog.Append(logParams.MessageType + ": " + logParams.Message);
            
            using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
            {
                sw.WriteLineAsync(strLog.ToString().Trim());
            }
        }
    }
}
