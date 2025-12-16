namespace StockPriceTickerApp.DTO
{
    public class Settings
    {
        public string? StockJsonPath { get; set; }
        public int StockRefreshRateInMiliSeconds { get; set; }
    }
}
