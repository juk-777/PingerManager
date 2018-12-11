using System.Collections.Generic;

namespace PingerManager.Config
{
    public interface IConfigVerifier
    {
        bool Verify(IEnumerable<ConfigEntity> configEntityList);
    }
}
