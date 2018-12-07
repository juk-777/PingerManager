using System;
using System.Threading;
using System.Threading.Tasks;

namespace PingerManager.BusinessLogic
{
    public interface IPingerBusinessLogic : IDisposable
    {
        Task StartJob(CancellationToken token);
    }
}
