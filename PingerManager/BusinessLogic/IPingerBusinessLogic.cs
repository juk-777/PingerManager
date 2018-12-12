using System;
using System.Threading;

namespace PingerManager.BusinessLogic
{
    public interface IPingerBusinessLogic : IDisposable
    {
        void StartJob(CancellationToken token);
    }
}
