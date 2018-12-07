using PingerManager.Constructor;

namespace PingerManager.Logging
{
    public interface ITxtLoggerWriter
    {
        void Write(PingReply pingReply);
    }
}
