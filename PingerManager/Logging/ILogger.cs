namespace PingerManager.Logging
{
    public interface ILogger
    {
        void Log(MessageType messageType, string message);
    }
}
