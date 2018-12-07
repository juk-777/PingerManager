namespace PingerManager.Logging
{
    public interface ILoggerProvider
    {
        void Log(MessageType messageType, string message);
    }
}
