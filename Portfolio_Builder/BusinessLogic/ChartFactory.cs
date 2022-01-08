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
        public SeriesCollection InitializeAssetChart(Asset asset)
        {
            ChartValues<DateTimePoint> chartValues = new ChartValues<DateTimePoint>();
            foreach (AssetDay assetDay in asset.MarketDays)
            {
                chartValues.Add(new DateTimePoint(assetDay.Date, assetDay.ClosingPrice));
            }

            SeriesCollection seriesCollection = new SeriesCollection()
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

        private LinearGradientBrush GetChartFill()
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush
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
