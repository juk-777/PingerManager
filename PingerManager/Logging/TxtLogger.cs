using System;

namespace PingerManager.Logging
{
    class TxtLogger : ILoggerProvider
    {
        //private static readonly ITxtLoggerWriter TxtLoggerWriter;

        //static TxtLogger()
        //{
        //    TxtLoggerWriter = new TxtLoggerWriter();
        //}

        //public static void Log(object sender, PingReply pingReply)
        //{
        //    TxtLoggerWriter.Write(pingReply);
        //}

        public void Log(MessageType messageType, string message)
        {
            Console.WriteLine(messageType + ": " + message + " - пишу в файл");
        }
    }
}
