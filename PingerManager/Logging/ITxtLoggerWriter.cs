using System.IO;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public interface ITxtLoggerWriter
    {
        Task WriteAsync(LogParams logParams, TextWriter textWriter);
    }
}
