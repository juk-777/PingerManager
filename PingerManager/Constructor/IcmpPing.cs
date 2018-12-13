using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace PingerManager.Constructor
{
    public class IcmpPing : IProtocolProvider
    {
        public async Task<PingReply> Ping(DateTime pingDate, PingEntity pingEntity)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping
                        .SendPingAsync(new UriBuilder(pingEntity.ConfigEntity.Host).Host, (int)TimeSpan.FromSeconds(pingEntity.ConfigEntity.Period).TotalMilliseconds);

                    return new PingReply(pingDate, pingEntity, reply.Status);
                }
            }
            catch (Exception)
            {
                return new PingReply(pingDate, pingEntity, IPStatus.BadOption);
            }
        }
    }
}
