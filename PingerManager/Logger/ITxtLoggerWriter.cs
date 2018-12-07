using PingerManager.Constructor;

namespace PingerManager.Logger
{
    public interface ITxtLoggerWriter
    {
        void Write(PingReply pingReply);
    }
}
