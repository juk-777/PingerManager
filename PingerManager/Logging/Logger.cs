using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class Logger : ILogger
    {
        public List<ILoggerProvider> Providers { get; set; }

        public void Log(MessageType messageType, string message)
        {
            Providers.ForEach(async p => await Task.Run(() => p.Log(messageType, message)));
        }
    }

    public enum MessageType
    {
        Info = 1,
        Warning,
        Error
    }
}
