using PingerManager.Logging;
using System.Collections.Generic;

namespace PingerManager.Config
{
    public interface IConfigReader
    {
        List<ConfigEntity> ReadConfig(ILogger logger);
    }
}
