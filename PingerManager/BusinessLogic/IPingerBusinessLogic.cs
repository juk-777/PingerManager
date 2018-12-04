using System;

namespace PingerManager.BusinessLogic
{
    public interface IPingerBusinessLogic : IDisposable
    {
        void StartJob();
    }
}
