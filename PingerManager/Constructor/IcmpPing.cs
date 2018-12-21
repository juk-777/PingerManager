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
                    var host = new UriBuilder(pingEntity.ConfigEntity.Host).Host;
                    var timeout = (int)TimeSpan.FromSeconds(2).TotalMilliseconds;
                    var reply = await ping.SendPingAsync(host, timeout);

                    return new PingReply(pingDate, pingEntity, reply.Status);
                }
            }
            catch (Exception e)
            {
                await logger.LogAsync(new LogParams(MessageType.Warning, e.Message));
                return null;
            }
        }
    }
}
