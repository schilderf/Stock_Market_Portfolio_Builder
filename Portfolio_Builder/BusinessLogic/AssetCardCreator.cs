﻿using Portfolio_Builder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.BusinessLogic
{
    public class AssetCardCreator
    {
        private DatabaseManagement databaseManagement = new DatabaseManagement();

        public AssetCardModel CreateAssetCard(string assetTickerSymbol)
        {
            Asset asset = databaseManagement.CreateAsset(assetTickerSymbol);

            AssetCardModel assetCardModel = new AssetCardModel();
            assetCardModel.Symbol = asset.Symbol;
            assetCardModel.Name = asset.Name;
            assetCardModel.MaxValues = FindMaxValues(assetTickerSymbol);
            assetCardModel.MinValues = FindMinValues(assetTickerSymbol);
            assetCardModel.PriceChanges = FindMaxValues(assetTickerSymbol);

            AssetDay mostRecentMarketDay = asset.MarketDays.Last();
            assetCardModel.CurrentPrice = mostRecentMarketDay.ClosingPrice;

            return assetCardModel;
        }
        private ObservableCollection<PerformanceCardModel> FindMaxValues(string assetTickerSymbol)
        {
            List<int> timeframes = new List<int> {1, 7, 30, 90, 180, 360, 1080, 3600 };

            ObservableCollection<PerformanceCardModel> results = new ObservableCollection<PerformanceCardModel>();

            foreach (int timeframe in timeframes )
            {
                DateTime dateTime = DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromDays(timeframe);
                PerformanceCardModel performanceCardModel = new PerformanceCardModel(databaseManagement.FindMaxValue(assetTickerSymbol, dateTime.Subtract(timeSpan)), GetTimeframeToPerformanceCardCaption(timeframe));
                results.Add(performanceCardModel);
            }
            return results;
        }

        private ObservableCollection<PerformanceCardModel> FindMinValues(string assetTickerSymbol)
        {
            List<int> timeframes = new List<int> { 1, 7, 30, 90, 180, 360, 1080, 3600 };

            ObservableCollection<PerformanceCardModel> results = new ObservableCollection<PerformanceCardModel>();

            foreach (int timeframe in timeframes)
            {
                DateTime dateTime = DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromDays(timeframe);
                PerformanceCardModel performanceCardModel = new PerformanceCardModel(databaseManagement.FindMinValue(assetTickerSymbol, dateTime.Subtract(timeSpan)), GetTimeframeToPerformanceCardCaption(timeframe));
                results.Add(performanceCardModel);
            }
            return results;
        }

        private string GetTimeframeToPerformanceCardCaption(int timeframe)
        {
            switch(timeframe)
            {
                case 1: return "1 Tag";
                case 7: return "1 Woche";
                case 30: return "1 Monat";
                case 90: return "3 Monate";
                case 180: return "6 Monate";
                case 360: return "1 Jahr";
                case 1080: return "3 Jahre";
                case 3600: return "10 Jahre";
                default: return "";
            }
        }
    }
}