using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using StockPriceTickerApp.Service;
using StockPriceTickerApp.DTO;

namespace StockPriceTickerTest.Helper
{
    public static class TestHelper
    {
        public static StockPriceService CreateService()
        {
            var hubMock = new Mock<IHubContext<StockHubDto>>();

            var envMock = new Mock<IHostEnvironment>();
            envMock.Setup(e => e.ContentRootPath).Returns("C:\\Test");

            var configMock = new Mock<IConfiguration>();

            var settings = new Settings
            {
                StockJsonPath = "Data/stocks.json",
                StockRefreshRateInMiliSeconds = 2000
            };

            var optionsMonitor = new Mock<IOptionsMonitor<Settings>>();
            optionsMonitor.Setup(o => o.CurrentValue).Returns(settings);

            return new StockPriceService(
                hubMock.Object,
                envMock.Object,
                configMock.Object,
                optionsMonitor.Object
            );
        }
    }
}
