namespace StockPriceTickerApp.Service
{
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Options;
    using StockPriceTickerApp.DTO;
    using StockPriceTickerApp.Models;
    using StockPriceTickerApp.Utility;
    using StockPriceTickerApp.Data.Enum;

    public class StockPriceService : BackgroundService
    {
        private readonly IHubContext<StockHubDto> _hub;
        private readonly IHostEnvironment _env;
        private readonly IConfiguration _config;
        private readonly Settings _settings;
        private readonly string _filePath;
        private readonly Random _random = new();


        public StockPriceService(IHubContext<StockHubDto> hub , IHostEnvironment env, IConfiguration config, IOptionsMonitor<Settings> settingsOption)
        {
            _hub = hub;
            _env = env;
            _config = config;
            _settings = settingsOption.CurrentValue;
            _filePath = Path.Combine(env.ContentRootPath, _settings.StockJsonPath);

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var stocks = await JsonReaderUtil.Load<List<StockDto>>(_filePath);
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var stock in stocks )
                {
                    var newStockPrice = await StockPriceGenerator.Generate();
                    UpdateStock(stock, newStockPrice);

                }

                await _hub.Clients.All.SendAsync(
                    "ReceiveStockUpdate",
                    stocks,
                    cancellationToken: stoppingToken
                );

                await Task.Delay(_settings.StockRefreshRateInMiliSeconds, stoppingToken); 
            }
   
    }

        public void UpdateStock(StockDto stock, double newStockPrice)
        {
            stock.DailyPriceAvg = stock.Price - newStockPrice;

            stock.DailyPercentageAvg = Math.Round(
                (stock.DailyPriceAvg / stock.Price) * 100, 2);

            stock.Indicator = stock.DailyPriceAvg > 0
                ? StockChangeIndicatorStatus.Increase
                : stock.DailyPriceAvg < 0
                    ? StockChangeIndicatorStatus.Decrease
                    : StockChangeIndicatorStatus.NoChanges;

            stock.Price = newStockPrice;
        }
    }
}
