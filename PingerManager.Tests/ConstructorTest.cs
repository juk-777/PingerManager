using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using PingerManager.Config;
using PingerManager.Constructor;
using PingerManager.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace PingerManager.Tests
{
    [TestClass]
    public class ConstructorTest
    {
        private ConfigEntity ConfigEntity { get; set; }
        private readonly ServiceProvider _serviceProvider;

        public ConstructorTest()
        {
            _serviceProvider = PingerServiceProvider.ServiceProvider;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            ConfigEntity = new ConfigEntity
            {
                Host = "ya.ru",
                Period = 2,
                Protocol = "ICMP",
                Port = 0,
                ValidStatusCode = 0,
                LogPath = "log_1.txt"
            };
        }

        [TestMethod]
        public void PingBuilder_ICMP_Provider_Is_Set()
        {
            var protocolProviders = _serviceProvider.GetServices<IProtocolProvider>().ToList();
            ConfigEntity.Protocol = "ICMP";
            var mockLogger = new Mock<ILogger>();
            var mockProtocolProvider = new Mock<IProtocolProvider>();
            PingEntity pingEntity = new PingEntity
            {
                ConfigEntity = ConfigEntity,
                ProtocolProvider = protocolProviders.First(o => o.GetType() == typeof(IcmpPing))
            };

            mockLogger.Setup(x => x.Log(It.IsAny<LogParams>()));
            mockProtocolProvider.Setup(x => x.Ping(It.IsAny<DateTime>(), pingEntity)).Returns(It.IsAny<Task<PingReply>>());


            var pingBuilder = new PingBuilder(mockLogger.Object);
            pingBuilder.BuildPing(ConfigEntity);

            mockProtocolProvider.Verify(x => x.Ping(It.IsAny<DateTime>(), pingEntity), Times.Once);
        }
    }
}
