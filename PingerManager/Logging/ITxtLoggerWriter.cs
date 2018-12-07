namespace PingerManager.Logging
{
    public interface ITxtLoggerWriter
    {
        void Write(MessageType messageType, string message);
    }
}
