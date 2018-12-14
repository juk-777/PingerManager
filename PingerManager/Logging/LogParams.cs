namespace PingerManager.Logging
{
    public class LogParams
    {
        public MessageType MessageType { get; }
        public string Message { get; }

        public LogParams(MessageType messageType, string message)
        {
            MessageType = messageType;
            Message = message;
        }   
    }
}
