using System;
using System.Threading;
using System.Threading.Tasks;
using PingerManager.Logging;

namespace PingerManager.BusinessLogic
{
    public interface IPingerBusinessLogic : IDisposable
    {
        Task StartJob(CancellationToken token, ILogger logger);
    }
}
