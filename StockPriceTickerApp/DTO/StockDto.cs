
namespace StockPriceTickerApp.Models
{
    using StockPriceTickerApp.Data.Enum;

    public class StockDto
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public double Price{ get; set; }
        public StockChangeIndicatorStatus Indicator { get; set; }
        public double DailyPercentageAvg { get; set; }
        public double DailyPriceAvg { get; set; }
    }
}
