using System.IO;
using System.Text;

namespace PingerManager.Logging
{
    public class TxtLoggerWriter : ITxtLoggerWriter
    {
        private readonly string _logPath;

        public TxtLoggerWriter(string logPath)
        {
            _logPath = logPath;
        }
        public void Write(MessageType messageType, string message)
        {
            string writePath = Path.Combine(_logPath);
            StringBuilder strLog = new StringBuilder();
            strLog.Append(messageType + ": " + message);
            
            using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
            {
                sw.WriteLineAsync(strLog.ToString().Trim());
            }
        }
    }
}
