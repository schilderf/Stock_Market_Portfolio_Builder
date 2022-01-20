using Portfolio_Builder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.BusinessLogic
{
    public class CardFactory
    {
        private readonly DatabaseManagement databaseManagement = new();
        private readonly ChartFactory chartFactory = new();

        public AssetCardModel CreateAssetCard(string assetTickerSymbol)
        {
            Asset asset = databaseManagement.CreateAsset(assetTickerSymbol);

            AssetCardModel assetCardModel = new();
            assetCardModel.Symbol = asset.Symbol;
            assetCardModel.Name = asset.Name;
            assetCardModel.PriceChart = chartFactory.InitializeAssetChart(asset);
            assetCardModel.PriceChanges = CalculatePerformances(assetTickerSymbol);
            assetCardModel.MaxValues = FindMaxValues(assetTickerSymbol);
            assetCardModel.MinValues = FindMinValues(assetTickerSymbol);

            AssetDay mostRecentMarketDay = asset.MarketDays.Last();
            assetCardModel.CurrentPrice = $"{mostRecentMarketDay.ClosingPrice}$";

            return assetCardModel;
        }
        private ObservableCollection<PerformanceCardModel> CalculatePerformances(string assetTickerSymbol)
        {
            ObservableCollection<PerformanceCardModel> results = new();

            foreach (int timeframe in GetTimeframes())
            {
                results.Add(CalculatePerformance(assetTickerSymbol, timeframe));
            }

            return results;
        }

        private PerformanceCardModel CalculatePerformance(string assetTickerSymbol, int timeFrame)
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromDays(timeFrame);
            DateTime compareDate = currentDate.Subtract(timeSpan);

            double result;
            try
            {
                result = databaseManagement.GetSingleClosingPrice(assetTickerSymbol, currentDate) / databaseManagement.GetSingleClosingPrice(assetTickerSymbol, compareDate) * 100;
            }
            catch (DivideByZeroException)
            {
                result = double.NaN;
            }
            return new PerformanceCardModel(Math.Round(result - 100,2), GetTimeframeToPerformanceCardCaption(timeFrame),"%");
        }
        private ObservableCollection<PerformanceCardModel> FindMaxValues(string assetTickerSymbol)
        {
            List<int> timeframes = GetTimeframes();

            ObservableCollection<PerformanceCardModel> results = new();

            foreach (int timeFrame in timeframes)
            {
                DateTime dateTime = DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromDays(timeFrame);
                PerformanceCardModel performanceCardModel = new(databaseManagement.FindMaxValue(assetTickerSymbol, dateTime.Subtract(timeSpan)), GetTimeframeToPerformanceCardCaption(timeFrame),"$");
                results.Add(performanceCardModel);
            }
            return results;
        }

        private ObservableCollection<PerformanceCardModel> FindMinValues(string assetTickerSymbol)
        {
            List<int> timeframes = GetTimeframes();

            ObservableCollection<PerformanceCardModel> results = new();

            foreach (int timeframe in timeframes)
            {
                DateTime dateTime = DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromDays(timeframe);
                PerformanceCardModel performanceCardModel = new(databaseManagement.FindMinValue(assetTickerSymbol, dateTime.Subtract(timeSpan)), GetTimeframeToPerformanceCardCaption(timeframe),"$");
                results.Add(performanceCardModel);
            }
            return results;
        }

        private static string GetTimeframeToPerformanceCardCaption(int timeframe)
        {
            return timeframe switch
            {
                2 => "1 Tag",
                7 => "1 Woche",
                30 => "1 Monat",
                90 => "3 Monate",
                180 => "6 Monate",
                360 => "1 Jahr",
                1080 => "3 Jahre",
                3600 => "10 Jahre",
                _ => ""
            };
        }

        private static List<int> GetTimeframes()
        {
            return new List<int> { 2, 7, 30, 90, 180, 360, 1080, 3600 };
        }
    }
}
