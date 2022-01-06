using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.BusinessLogic
{
    public class AssetDay
    {
        private DateTime _date;

        private double _openingPrice;
        public double OpeningPrice
        {
            get => _openingPrice;
        }
        private double _closingPrice;
        public double ClosingPrice
        {
            get => _closingPrice;
        }
        private double _dailyHigh;
        public double DailyHigh
        {
            get => _dailyHigh;
        }
        private double _dailyLow;
        public double DailyLow
        {
            get => _dailyLow;
        }
        private double _dailyVolume;
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
