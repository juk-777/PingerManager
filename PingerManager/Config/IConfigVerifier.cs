using System.Collections.Generic;

namespace PingerManager.Config
{
    public interface IConfigVerifier
    {
        bool Verify(List<ConfigEntity> configEntityList);
    }
}
