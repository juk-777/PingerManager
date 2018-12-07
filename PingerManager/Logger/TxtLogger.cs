using PingerManager.Constructor;

namespace PingerManager.Logger
{
    static class TxtLogger
    {
        private static readonly ITxtLoggerWriter TxtLoggerWriter;

        static TxtLogger()
        {
            TxtLoggerWriter = new TxtLoggerWriter();
        }

        public static void Log(object sender, PingReply pingReply)
        {
            TxtLoggerWriter.Write(pingReply);
        }
    }
}
