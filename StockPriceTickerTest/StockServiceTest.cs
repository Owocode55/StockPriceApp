using StockPriceTickerApp.Data.Enum;
using StockPriceTickerApp.Models;
using StockPriceTickerTest.Helper;

namespace StockPriceTickerTest
{
    public class StockServiceTest
    {
        [Fact]
        public void UpdateStock_WhenPriceDecreases_ShouldSetIncreaseIndicator()
        {
            // Arrange
            var stock = new StockDto
            {
                Price = 100
            };

            var service = TestHelper.CreateService();

            // Act
            service.UpdateStock(stock, 90);

            // Assert
            Assert.Equal(10, stock.DailyPriceAvg);
            Assert.Equal(10.00, stock.DailyPercentageAvg);
            Assert.Equal(StockChangeIndicatorStatus.Increase, stock.Indicator);
            Assert.Equal(90.00, stock.Price);
        }

        [Fact]
        public void UpdateStock_WhenPriceIncreases_ShouldSetDecreaseIndicator()
        {
            var stock = new StockDto { Price = 100 };
            var service = TestHelper.CreateService();

            service.UpdateStock(stock, 110);

            Assert.Equal(-10, stock.DailyPriceAvg);
            Assert.Equal(-10.00, stock.DailyPercentageAvg);
            Assert.Equal(StockChangeIndicatorStatus.Decrease, stock.Indicator);
        }

        [Fact]
        public void UpdateStock_WhenPriceUnchanged_ShouldSetNoChangeIndicator()
        {
            var stock = new StockDto { Price = 100 };
            var service = TestHelper.CreateService();

            service.UpdateStock(stock, 100);

            Assert.Equal(0, stock.DailyPriceAvg);
            Assert.Equal(0.00, stock.DailyPercentageAvg);
            Assert.Equal(StockChangeIndicatorStatus.NoChanges, stock.Indicator);
        }
    }
}