using PingerManager.Logging;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace PingerManager.Constructor
{
    public class IcmpPing : IProtocolProvider
    {
        public async Task<PingReply> PingAsync(DateTime pingDate, PingEntity pingEntity, ILogger logger)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(new UriBuilder(pingEntity.ConfigEntity.Host).Host, (int)TimeSpan.FromSeconds(2).TotalMilliseconds);

                    return new PingReply(pingDate, pingEntity, reply.Status);
                }
            }
            catch (Exception e)
            {
                logger.Log(new LogParams(MessageType.Warning, e.Message));
                return new PingReply(pingDate, pingEntity, IPStatus.BadOption);
            }
        }
    }
}
