using System;
using System.Collections.Generic;
using System.Text;

namespace PingerManager.Logger
{
    public interface ILoggerProvider
    {
        void Log(string message);
    }
}
