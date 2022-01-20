using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using LiveCharts;
using System.Collections.ObjectModel;
using LiveCharts.Wpf;
using Microsoft.Toolkit.Mvvm.Input;

namespace Portfolio_Builder.Models
{
    public class AssetCardModel : ObservableObject
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

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private readonly int _nameFontSize;
        public int NameFontSize
        {
            get => _nameFontSize;
        }

        private string _currentPrice;
        public string CurrentPrice
        {
            get => _currentPrice;
            set => SetProperty(ref _currentPrice, value);
        }

        private readonly int _currentPriceFontSize;
        public int CurrentPriceFontSize
        {
            get => _currentPriceFontSize;
        }

        private readonly string _currentPriceCaption;
        public string CurrentPriceCaption
        {
            get => _currentPriceCaption;
        }

        private SeriesCollection _priceChart;
        public SeriesCollection PriceChart
        {
            get => _priceChart;
            set => SetProperty(ref _priceChart, value);
        }

        private ObservableCollection<PerformanceCardModel> _priceChanges;
        public ObservableCollection<PerformanceCardModel> PriceChanges
        {
            get => _priceChanges;
            set => SetProperty(ref _priceChanges, value);
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

        private long _separatorStep;
        public long SeparatorStep
        {
            get => _separatorStep;
            set => SetProperty(ref _separatorStep, value);
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

        public AssetCardModel()
        {
            _symbol = "";
            _name = "";
            _currentPrice = "";
            _priceChart = new SeriesCollection();
            _priceChanges = new ObservableCollection<PerformanceCardModel>();
            _maxValues = new ObservableCollection<PerformanceCardModel>();
            _minValues = new ObservableCollection<PerformanceCardModel>();

            _symbolFontSize = 25;
            _nameFontSize = 15;
            _currentPriceFontSize = 30;
            _currentPriceCaption = "Aktueller Preis:";

            _setTimeFrame3M = new RelayCommand(() => SetTimeFrame(90));
            _setTimeFrame6M = new RelayCommand(() => SetTimeFrame(180));
            _setTimeFrame1Y = new RelayCommand(() => SetTimeFrame(365));
            _setTimeFrame3Y = new RelayCommand(() => SetTimeFrame(1095));
            _setTimeFrame5Y = new RelayCommand(() => SetTimeFrame(1825));
            _setTimeFrame10Y = new RelayCommand(() => SetTimeFrame(3650));

            _maxXChartValue = DateTime.Now.Ticks;
            _minXChartValue = DateTime.Now.Subtract(new TimeSpan(365,0,0,0)).Ticks;
            _separatorStep = TimeSpan.FromDays(90).Ticks;
            XFormatter = val => new DateTime((long)val).ToString("MMM yyyy");
            YFormatter = val => val.ToString("C");
        }

        public AssetCardModel(string symbol, string name, SeriesCollection priceChart) : this()
        {
            _symbol = symbol;
            _name = name;
            _priceChart = priceChart;
            _priceChanges = new ObservableCollection<PerformanceCardModel>();
            _maxValues = new ObservableCollection<PerformanceCardModel>();
            _minValues = new ObservableCollection<PerformanceCardModel>();
        }

        private void SetTimeFrame(int days)
        {
            MaxXChartValue = default;
            MinXChartValue = default;

            MaxXChartValue = DateTime.Now.Ticks;
            MinXChartValue = DateTime.Now.Subtract(new TimeSpan(days,0,0,0)).Ticks;

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
