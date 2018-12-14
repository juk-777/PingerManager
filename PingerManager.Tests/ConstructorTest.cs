using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
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

        [TestInitialize]
        public void TestInitialize()
        {
            ConfigEntity = new ConfigEntity
            {
                Host = "ya.ru",
                Period = 2,
                Protocol = "ICMP",
                Port = 0,
                ValidStatusCode = 0
            };
        }

        [TestMethod]
        public void ProtocolProviderManager_ICMP_Provider_Is_Set()
        {
            ConfigEntity.Protocol = "ICMP";
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.LogAsync(It.IsAny<LogParams>())).Returns(Task.CompletedTask);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IProtocolProviderManager>(p => new ProtocolProviderManager(mockLogger.Object))
                .BuildServiceProvider();

            var protocolProviderManager = serviceProvider.GetService<IProtocolProviderManager>();
            var protocolProvider = protocolProviderManager.GetProvider(ConfigEntity);

            Assert.IsInstanceOf<IcmpPing>(protocolProvider);
        }

        [TestMethod]
        public void ProtocolProviderManager_TCP_Provider_Is_Set()
        {
            ConfigEntity.Protocol = "TCP";
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.LogAsync(It.IsAny<LogParams>())).Returns(Task.CompletedTask);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IProtocolProviderManager>(p => new ProtocolProviderManager(mockLogger.Object))
                .BuildServiceProvider();

            var protocolProviderManager = serviceProvider.GetService<IProtocolProviderManager>();
            var protocolProvider = protocolProviderManager.GetProvider(ConfigEntity);

            Assert.IsInstanceOf<TcpPing>(protocolProvider);
        }

        [TestMethod]
        public void ProtocolProviderManager_HTTP_Provider_Is_Set()
        {
            ConfigEntity.Protocol = "HTTP";
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.LogAsync(It.IsAny<LogParams>())).Returns(Task.CompletedTask);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IProtocolProviderManager>(p => new ProtocolProviderManager(mockLogger.Object))
                .BuildServiceProvider();

            var protocolProviderManager = serviceProvider.GetService<IProtocolProviderManager>();
            var protocolProvider = protocolProviderManager.GetProvider(ConfigEntity);

            Assert.IsInstanceOf<HttpPing>(protocolProvider);
        }

        [TestMethod]
        public async Task HTTP_Ping_Return_BadOption_Status_When_ValidStatusCode_Is_Not_200()
        {
            ConfigEntity.Protocol = "HTTP";
            ConfigEntity.ValidStatusCode = 201;
            var mockProtocolProviderManager = new Mock<IProtocolProviderManager>();
            mockProtocolProviderManager
                .Setup(x => x.GetProvider(It.IsAny<ConfigEntity>()))
                .Returns(new HttpPing());

            var pingEntity = new PingEntity
            {
                ConfigEntity = ConfigEntity,
                ProtocolProvider = mockProtocolProviderManager.Object.GetProvider(It.IsAny<ConfigEntity>())
            };

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IProtocolProvider, HttpPing>()
                .BuildServiceProvider();

            var httpPing = serviceProvider.GetService<IProtocolProvider>();
            var pingReply = await httpPing.PingAsync(DateTime.Now, pingEntity);

            Assert.AreEqual(IPStatus.BadOption, pingReply.Status);
        }

        [TestMethod]
        public async Task Benchmark_Test_Yandex_HTTP_Ping_Return_Success_Status_When_ValidStatusCode_Is_200()
        {
            ConfigEntity.Host = "ya.ru";
            ConfigEntity.Protocol = "HTTP";
            ConfigEntity.ValidStatusCode = 200;

            var mockProtocolProviderManager = new Mock<IProtocolProviderManager>();
            mockProtocolProviderManager
                .Setup(x => x.GetProvider(It.IsAny<ConfigEntity>()))
                .Returns(new HttpPing());

            var pingEntity = new PingEntity
            {
                ConfigEntity = ConfigEntity,
                ProtocolProvider = mockProtocolProviderManager.Object.GetProvider(It.IsAny<ConfigEntity>())
            };

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IProtocolProvider, HttpPing>()
                .BuildServiceProvider();

            var httpPing = serviceProvider.GetService<IProtocolProvider>();
            var pingReply = await httpPing.PingAsync(DateTime.Now, pingEntity);

            Assert.AreEqual(IPStatus.Success, pingReply.Status);
        }

        [TestMethod]
        public async Task Benchmark_Test_Yandex_ICMP_Ping_Return_Success_Status()
        {
            ConfigEntity.Host = "ya.ru";
            ConfigEntity.Protocol = "ICMP";

            var mockProtocolProviderManager = new Mock<IProtocolProviderManager>();
            mockProtocolProviderManager
                .Setup(x => x.GetProvider(It.IsAny<ConfigEntity>()))
                .Returns(new IcmpPing());

            var pingEntity = new PingEntity
            {
                ConfigEntity = ConfigEntity,
                ProtocolProvider = mockProtocolProviderManager.Object.GetProvider(It.IsAny<ConfigEntity>())
            };

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IProtocolProvider, IcmpPing>()
                .BuildServiceProvider();

            var icmpPing = serviceProvider.GetService<IProtocolProvider>();
            var pingReply = await icmpPing.PingAsync(DateTime.Now, pingEntity);

            Assert.AreEqual(IPStatus.Success, pingReply.Status);
        }
    }
}
