using LiveCharts;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.Models
{
    public class MarketCardModel : ObservableObject
    {
        private string _symbol;
        public string Symbol
        {
            get => _symbol;
            set => SetProperty(ref _symbol, value);
        }

        private readonly int _symbolFontSize;
        public int SymbolFontSize
        {
            get => _symbolFontSize;
        }

        private SeriesCollection _chart;
        public SeriesCollection Chart
        {
            get => _chart;
            set => SetProperty(ref _chart, value);
        }


        private ObservableCollection<PerformanceCardModel> _changes;
        public ObservableCollection<PerformanceCardModel> Changes
        {
            get => _changes;
            set => SetProperty(ref _changes, value);
        }

        private ObservableCollection<PerformanceCardModel> _maxValues;
        public ObservableCollection<PerformanceCardModel> MaxValues
        {
            get => _maxValues;
            set => SetProperty(ref _maxValues, value);
        }

        private ObservableCollection<PerformanceCardModel> _minValues;
        public ObservableCollection<PerformanceCardModel> MinValues
        {
            get => _minValues;
            set => SetProperty(ref _minValues, value);
        }

        private long _separatorStep;
        public long SeparatorStep
        {
            get => _separatorStep;
            set => SetProperty(ref _separatorStep, value);
        }
        public Func<double, string> XFormatter { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private long _maxXChartValue;
        public long MaxXChartValue
        {
            get => _maxXChartValue;
            set => SetProperty(ref _maxXChartValue, value);
        }

        private long _minXChartValue;
        public long MinXChartValue
        {
            get => _minXChartValue;
            set => SetProperty(ref _minXChartValue, value);
        }

        private readonly RelayCommand _setTimeFrame3M;
        public RelayCommand SetTimeFrame3M
        {
            get => _setTimeFrame3M;
        }

        private readonly RelayCommand _setTimeFrame6M;
        public RelayCommand SetTimeFrame6M
        {
            get => _setTimeFrame6M;
        }

        private readonly RelayCommand _setTimeFrame1Y;
        public RelayCommand SetTimeFrame1Y
        {
            get => _setTimeFrame1Y;
        }

        private readonly RelayCommand _setTimeFrame3Y;
        public RelayCommand SetTimeFrame3Y
        {
            get => _setTimeFrame3Y;
        }

        private readonly RelayCommand _setTimeFrame5Y;
        public RelayCommand SetTimeFrame5Y
        {
            get => _setTimeFrame5Y;
        }

        private readonly RelayCommand _setTimeFrame10Y;
        public RelayCommand SetTimeFrame10Y
        {
            get => _setTimeFrame10Y;
        }

        public MarketCardModel()
        {
            _symbol = "";
            _symbolFontSize = 25;
            _chart = new SeriesCollection();
            _changes = new ObservableCollection<PerformanceCardModel>();
            _maxValues = new ObservableCollection<PerformanceCardModel>();
            _minValues = new ObservableCollection<PerformanceCardModel>();

            _setTimeFrame3M = new RelayCommand(() => SetTimeFrame(90));
            _setTimeFrame6M = new RelayCommand(() => SetTimeFrame(180));
            _setTimeFrame1Y = new RelayCommand(() => SetTimeFrame(365));
            _setTimeFrame3Y = new RelayCommand(() => SetTimeFrame(1095));
            _setTimeFrame5Y = new RelayCommand(() => SetTimeFrame(1825));
            _setTimeFrame10Y = new RelayCommand(() => SetTimeFrame(3650));

            _maxXChartValue = DateTime.Now.Ticks;
            _minXChartValue = DateTime.Now.Subtract(new TimeSpan(365, 0, 0, 0)).Ticks;
            _separatorStep = TimeSpan.FromDays(90).Ticks;
            XFormatter = val => new DateTime((long)val).ToString("MMM yyyy");
            YFormatter = val => val.ToString("C");
        }


        private void SetTimeFrame(int days)
        {
            MaxXChartValue = default;
            MinXChartValue = default;

            MaxXChartValue = DateTime.Now.Ticks;
            MinXChartValue = DateTime.Now.Subtract(new TimeSpan(days, 0, 0, 0)).Ticks;

            if (days <= 180)
            {
                SeparatorStep = TimeSpan.FromDays(30).Ticks;
                XFormatter = val => new DateTime((long)val).ToString("MMM yyyy");

                return;
            }
            if (days <= 365)
            {
                SeparatorStep = TimeSpan.FromDays(90).Ticks;
                XFormatter = val => new DateTime((long)val).ToString("MMM yyyy");

                return;
            }
            if (days <= 1825)
            {
                SeparatorStep = TimeSpan.FromDays(365).Ticks;
                XFormatter = val => new DateTime((long)val).ToString("yyyy");

                return;
            }
            if (days <= 3650)
            {
                SeparatorStep = TimeSpan.FromDays(730).Ticks;
                XFormatter = val => new DateTime((long)val).ToString("yyyy");

                return;
            }

        }
    }
}
