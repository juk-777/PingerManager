using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class Logger : ILogger
    {
        public List<ILoggerProvider> Providers { get; }

        public Logger()
        {
            Providers = new List<ILoggerProvider>();
        }

        public void Log(LogParams logParams)
        {
            Providers.ForEach(async p => await Task.Run(() => p.Log(logParams)));
        }
    }

    public enum MessageType
    {
        Info = 1,
        Error
    }
}
