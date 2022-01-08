using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using LiveCharts;
using System.Collections.ObjectModel;

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

        private int _symbolFontSize;
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

        private int _nameFontSize;
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

        private int _currentPriceFontSize;
        public int CurrentPriceFontSize
        {
            get => _currentPriceFontSize;
        }

        private string _currentPriceCaption;
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
    }
}
