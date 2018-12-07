using System.IO;
using System.Text;
using PingerManager.Constructor;

namespace PingerManager.Logger
{
    public class TxtLoggerWriter : ITxtLoggerWriter
    {
        public void Write(PingReply pingReply)
        {
            string writePath = Path.Combine(pingReply.ConfigEntity.Host + "_" + pingReply.ConfigEntity.Protocol + "_log" + ".txt");

            StringBuilder strLog = new StringBuilder();
            strLog.Append(pingReply.PingDate + " " + pingReply.ConfigEntity.Host + " " + pingReply.Status);
            
            using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
            {
                sw.WriteLineAsync(strLog.ToString().Trim());
            }
        }
    }
}
