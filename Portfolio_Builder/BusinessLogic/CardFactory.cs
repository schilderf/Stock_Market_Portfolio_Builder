using Portfolio_Builder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Portfolio_Builder.BusinessLogic
{
    public class CardFactory
    {
        private readonly DatabaseManagement databaseManagement = new();

        public ObservableCollection<AssetCardModel> CreateWatchlistAssetCardCollection()
        {
            ObservableCollection<AssetCardModel> assetCardModels = new();
            foreach (string assetSymbol in databaseManagement.GetItemsOnWatchlist("Asset"))
            {
                assetCardModels.Add(CreateAssetCard(assetSymbol));
            }
            return assetCardModels;
        }

        public ObservableCollection<MarketCardModel> CreateWatchlistMarketCardCollection()
        {
            ObservableCollection<MarketCardModel> marketCardModels = new();
            foreach (string name in databaseManagement.GetItemsOnWatchlist("Market"))
            {
                marketCardModels.Add(CreateMarketCard(name));
            }
            return marketCardModels;
        }
        public AssetCardModel CreateAssetCard(string assetTickerSymbol)
        {
            Asset asset = databaseManagement.CreateAsset(assetTickerSymbol);

            AssetCardModel assetCardModel = new();
            assetCardModel.Symbol = asset.Symbol;
            assetCardModel.Name = asset.Name;
            assetCardModel.PriceChart = ChartFactory.InitializeAssetChart(asset);
            assetCardModel.PriceChanges = CalculatePerformances(assetTickerSymbol, "Asset");
            assetCardModel.MaxValues = FindMaxValues(assetTickerSymbol, "Asset");
            assetCardModel.MinValues = FindMinValues(assetTickerSymbol, "Asset");

            AssetDay mostRecentMarketDay = asset.MarketDays.Last();
            assetCardModel.CurrentPrice = $"{mostRecentMarketDay.ClosingPrice}$";

            return assetCardModel;
        }

        public MarketCardModel CreateMarketCard(string marketName)
        {
            Market market = databaseManagement.CreateMarket(marketName);
            MarketCardModel marketCardModel = new();
            marketCardModel.Name = market.Name;
            marketCardModel.Chart = ChartFactory.InitializeMarketChart(market);
            marketCardModel.Changes = CalculatePerformances(marketName, "Market");
            marketCardModel.MaxValues = FindMaxValues(marketName, "Market");
            marketCardModel.MinValues = FindMinValues(marketName, "Market");
            marketCardModel.Assets = market.Assets;

            MarketDay mostRecentDay = market.Days.Last();
            marketCardModel.CurrentValue = $"{mostRecentDay.Value}$";

            return marketCardModel;
        }
        private ObservableCollection<PerformanceCardModel> CalculatePerformances(string assetTickerSymbol, string type)
        {
            ObservableCollection<PerformanceCardModel> results = new();

            foreach (int timeframe in GetTimeframes())
            {
                results.Add(CalculatePerformance(assetTickerSymbol, type, timeframe));
            }

            return results;
        }

        private PerformanceCardModel CalculatePerformance(string symbol, string type, int timeFrame)
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromDays(timeFrame);
            DateTime compareDate = currentDate.Subtract(timeSpan);

            double result;
            try
            {
                if (type == "Asset")
                    result = databaseManagement.GetSingleClosingPrice(symbol, currentDate) / databaseManagement.GetSingleClosingPrice(symbol, compareDate) * 100;
                else if (type == "Market")
                    result = databaseManagement.GetCurrentPerformance(symbol, currentDate) / databaseManagement.GetCurrentPerformance(symbol, compareDate) * 100;
                else
                    result = double.NaN;
            }
            catch (DivideByZeroException)
            {
                result = double.NaN;
            }
            return new PerformanceCardModel(Math.Round(result - 100,2), GetTimeframeToPerformanceCardCaption(timeFrame),"%");
        }
        private ObservableCollection<PerformanceCardModel> FindMaxValues(string symbol, string type)
        {
            List<int> timeframes = GetTimeframes();

            ObservableCollection<PerformanceCardModel> results = new();

            foreach (int timeFrame in timeframes)
            {
                DateTime dateTime = DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromDays(timeFrame);
                PerformanceCardModel performanceCardModel;
                if (type == "Asset")
                    performanceCardModel = new(databaseManagement.FindMaxValue(symbol, dateTime.Subtract(timeSpan)), GetTimeframeToPerformanceCardCaption(timeFrame), "$");
                else if (type == "Market")
                    performanceCardModel = new(databaseManagement.FindMaxMarketValue(symbol, dateTime.Subtract(timeSpan)), GetTimeframeToPerformanceCardCaption(timeFrame), "$");
                else
                    performanceCardModel = new(0, GetTimeframeToPerformanceCardCaption(timeFrame),"");

                results.Add(performanceCardModel);
            }
            return results;
        }

        private ObservableCollection<PerformanceCardModel> FindMinValues(string symbol, string type)
        {
            List<int> timeframes = GetTimeframes();

            ObservableCollection<PerformanceCardModel> results = new();

            foreach (int timeFrame in timeframes)
            {
                DateTime dateTime = DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromDays(timeFrame);
                PerformanceCardModel performanceCardModel;
                if (type == "Asset")
                    performanceCardModel = new(databaseManagement.FindMinValue(symbol, dateTime.Subtract(timeSpan)), GetTimeframeToPerformanceCardCaption(timeFrame),"$");
                else if (type == "Market")
                    performanceCardModel = new(databaseManagement.FindMinMarketValue(symbol, dateTime.Subtract(timeSpan)), GetTimeframeToPerformanceCardCaption(timeFrame), "$");
                else
                    performanceCardModel = new(0, GetTimeframeToPerformanceCardCaption(timeFrame), "");

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
