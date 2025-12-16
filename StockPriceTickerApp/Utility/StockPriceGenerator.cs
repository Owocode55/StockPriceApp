using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockPriceTickerApp.Utility
{
    public static class StockPriceGenerator
    {
        public static async Task<double> Generate()
        {
            double min = 1.0;
            double max = 2000.0;
            Random random = new Random();
            double value = random.NextDouble() * (max - min) + min;

            return Math.Round(value, 2);

        }
    }
}
