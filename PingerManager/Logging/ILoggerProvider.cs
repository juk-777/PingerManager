using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public interface ILoggerProvider
    {
        Task Log(LogParams logParams);
    }
}
