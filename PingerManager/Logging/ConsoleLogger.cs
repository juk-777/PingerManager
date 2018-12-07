using System;

namespace PingerManager.Logging
{
    public class ConsoleLogger : ILoggerProvider
    {
        //public static void Log(object sender, PingReply pingReply)
        //{
        //    Console.WriteLine(pingReply.PingDate + " " + pingReply.ConfigEntity.Host + " " + pingReply.Status);
        //}

        public void Log(MessageType messageType, string message)
        {
            Console.WriteLine(messageType + ": " + message);
        }
    }
}
