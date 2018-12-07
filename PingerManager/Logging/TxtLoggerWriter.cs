using System.IO;
using System.Text;

namespace PingerManager.Logging
{
    public class TxtLoggerWriter : ITxtLoggerWriter
    {
        public void Write(MessageType messageType, string message)
        {
            //string writePath = Path.Combine("log_" + pingReply.ConfigEntity.Host + "_" + pingReply.ConfigEntity.Protocol + ".txt");
            string writePath = Path.Combine("log_pingers" + ".txt");

            StringBuilder strLog = new StringBuilder();
            strLog.Append(messageType + ": " + message);
            
            using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
            {
                sw.WriteLineAsync(strLog.ToString().Trim());
            }
        }
    }
}
