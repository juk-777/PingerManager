using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace PingerManager.Config
{
    public interface IConfigStream
    {
        List<ConfigEntity> ReadStream(IConfiguration configuration);
    }
}
