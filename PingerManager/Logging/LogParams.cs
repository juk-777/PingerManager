namespace PingerManager.Logging
{
    public class LogParams
    {
        public MessageType MessageType { get; }
        public string Message { get; }
        public string LogPath { get; }

        public LogParams(MessageType messageType, string message, string logPath)
        {
            MessageType = messageType;
            Message = message;
            LogPath = logPath;
        }   
    }
}
