using System.Text.Json;

namespace StockPriceTickerApp.Utility
{
    public static class JsonReaderUtil
    {
        public static async Task<T> Load<T>(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("stocks.json not found");

            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<T>(json);
                    
        }
    }
}
