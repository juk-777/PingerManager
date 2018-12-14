using System.IO;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public interface ITxtLoggerWriter
    {
        Task Write(LogParams logParams, TextWriter textWriter);
    }
}
