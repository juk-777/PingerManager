using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace PingerManager.Config
{
    public interface IConfigReader
    {
        List<ConfigEntity> ReadConfig(IConfiguration configuration);
    }
}
