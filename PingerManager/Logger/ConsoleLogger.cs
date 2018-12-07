using System;
using PingerManager.Constructor;

namespace PingerManager.Logger
{
    static class ConsoleLogger
    {
        public static void Log(object sender, PingReply pingReply)
        {
            Console.WriteLine(pingReply.PingDate + " " + pingReply.ConfigEntity.Host + " " + pingReply.Status);
        }
    }
}
