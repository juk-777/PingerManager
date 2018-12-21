using Microsoft.Extensions.Configuration;
using System;
using System.Threading;

namespace PingerManager.BusinessLogic
{
    public interface IPingerBusinessLogic : IDisposable
    {
        void StartJob(IConfiguration configuration, CancellationToken token);
    }
}
