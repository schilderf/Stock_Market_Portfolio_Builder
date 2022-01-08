using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.BusinessLogic
{
    public class AssetDay
    {
        private readonly DateTime _date;
        public DateTime Date
        {
            get => _date;
        }
        private readonly double _openingPrice;
        public double OpeningPrice
        {
            get => _openingPrice;
        }
        private readonly double _closingPrice;
        public double ClosingPrice
        {
            get => _closingPrice;
        }
        private readonly double _dailyHigh;
        public double DailyHigh
        {
            get => _dailyHigh;
        }
        private readonly double _dailyLow;
        public double DailyLow
        {
            get => _dailyLow;
        }
        private readonly double _dailyVolume;
        public double DailyVolume
        {
            get => _dailyVolume;
        }

        public AssetDay()
        {
            _date = new DateTime();
            _openingPrice = 0.00;
            _closingPrice = 0.00;
            _dailyHigh = 0.00;
            _dailyLow = 0.00;
            _dailyVolume = 0.00;
        }

        public AssetDay(DateTime date, double openingPrice, double closingPrice, double dailyHigh, double dailyLow, double volume)
        {
            _date = date;
            _openingPrice = openingPrice;
            _closingPrice = closingPrice;
            _dailyHigh = dailyHigh;
            _dailyLow = dailyLow;
            _dailyVolume = volume;
        }
    }
}
