using PingerManager.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace PingerManager.Constructor
{
    public class HttpPing : IProtocolProvider
    {
        public async Task<PingReply> PingAsync(DateTime pingDate, PingEntity pingEntity, ILogger logger)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var reply = new HttpRequestMessage
                    {
                        RequestUri = new UriBuilder(pingEntity.ConfigEntity.Host).Uri
                    };

                    var response = await httpClient.SendAsync(reply);

                    if (response.StatusCode == (HttpStatusCode)pingEntity.ConfigEntity.ValidStatusCode)
                    {
                        return new PingReply(pingDate, pingEntity, IPStatus.Success);
                    }

                    return new PingReply(pingDate, pingEntity, IPStatus.BadOption);
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
