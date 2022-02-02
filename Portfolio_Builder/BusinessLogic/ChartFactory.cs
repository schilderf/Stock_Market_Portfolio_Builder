using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Portfolio_Builder.BusinessLogic
{
    public class ChartFactory
    {
        public static SeriesCollection InitializeAssetChart(Asset asset)
        {
            ChartValues<DateTimePoint> chartValues = new();
            foreach (AssetDay assetDay in asset.MarketDays)
            {
                chartValues.Add(new DateTimePoint(assetDay.Date, assetDay.ClosingPrice));
            }

            SeriesCollection seriesCollection = new()
            {
                new LineSeries
                {
                    Title = "ClosingPrices",
                    Fill = GetChartFill(),
                    Values = chartValues,
                    PointGeometry = null
                }
            };

            return seriesCollection;
        }

        public static SeriesCollection InitializeMarketChart(Market market)
        {
            ChartValues<DateTimePoint> chartValues = new();
            foreach (MarketDay marketDay in market.Days)
            {
                chartValues.Add(new DateTimePoint(marketDay.Date, marketDay.Value));
            }

            SeriesCollection seriesCollection = new()
            {
                new LineSeries
                {
                    Title = "ClosingPrices",
                    Values = chartValues,
                    PointGeometry = null
                }
            };
            
            return seriesCollection;
        }

        public static LineSeries AddAssetChart(Asset asset, SeriesCollection seriesCollection)
        {
            ChartValues<DateTimePoint> chartValues = new();
            foreach (AssetDay assetDay in asset.MarketDays)
            {
                chartValues.Add(new DateTimePoint(assetDay.Date, assetDay.ClosingPrice));
            }

            LineSeries series = new()
            {
                Title = "ClosingPrices",
                Values = chartValues,
                PointGeometry = null
            };

            seriesCollection.Add(series);
            return series;
        }

        public static void RemoveAssetChart(LineSeries series, SeriesCollection seriesCollection)
        {
            seriesCollection.Remove(series);
        }

        private static LinearGradientBrush GetChartFill()
        {
            LinearGradientBrush gradientBrush = new()
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(5, 0, 100), 0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1));

            return gradientBrush;
        }
    }
}
