using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using PingerManager.Config;

namespace PingerManager.Constructor
{
    public class IcmpPing : IProtocolProvider
    {
        public async Task<PingReply> Ping(DateTime pingDate, ConfigEntity configEntity)
        {
            using (Ping ping = new Ping())
            {
                var reply = await ping
                    .SendPingAsync(new UriBuilder(configEntity.Host).Host, (int)TimeSpan.FromSeconds(configEntity.Period).TotalMilliseconds);

                return new PingReply(pingDate, configEntity, reply.Status);
            }
        }
    }
}
