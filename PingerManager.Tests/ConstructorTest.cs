using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PingerManager.Config;
using PingerManager.Constructor;
using PingerManager.Logging;
using Assert = NUnit.Framework.Assert;

namespace PingerManager.Tests
{
    [TestClass]
    public class ConstructorTest
    {
        private ConfigEntity ConfigEntity { get; set; }
        //private ServiceProvider _serviceProvider;

        //public ConstructorTest()
        //{
        //    _serviceProvider = PingerServiceProvider.ServiceProvider;
        //}

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
            ConfigEntity.Protocol = "ICMP";
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.Log(It.IsAny<LogParams>()));
            var mockProtocolProviderManager = new Mock<IProtocolProviderManager>();

            var protocolProviderManager = new ProtocolProviderManager(mockLogger.Object);
            var protocolProvider = protocolProviderManager.GetProvider(ConfigEntity);
            var pingBuilder = new PingBuilder(mockLogger.Object, mockProtocolProviderManager.Object);
            pingBuilder.BuildPing(ConfigEntity);

            Assert.IsInstanceOf<IcmpPing>(protocolProvider);
        }
    }
}
