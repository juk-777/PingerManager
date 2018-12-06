using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using PingerManager.Config;

namespace PingerManager.Constructor
{
    class HttpPing : IProtocolProvider
    {
        public async Task<PingReply> Ping(DateTime pingDate, ConfigEntity configEntity)
        {
            var request = (HttpWebRequest)WebRequest.Create(new UriBuilder(configEntity.Host).Uri);
            request.Timeout = (int)TimeSpan.FromSeconds(configEntity.Period).TotalMilliseconds;

            try
            {
                using (var reply = (HttpWebResponse)await request.GetResponseAsync())
                {
                    if (reply.StatusCode == (HttpStatusCode)configEntity.ValidStatusCode)
                        return new PingReply(pingDate, configEntity, IPStatus.Success);

                    return new PingReply(pingDate, configEntity, IPStatus.BadOption);
                }
            }
            catch (WebException)
            {
                return new PingReply(pingDate, configEntity, IPStatus.BadOption);
            }
        }
    }
}
