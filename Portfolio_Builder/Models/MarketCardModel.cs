﻿using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Portfolio_Builder.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.Models
{
    public class MarketCardModel : ObservableRecipient
    {
        private readonly DatabaseManagement databaseManagement = new();
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

        private SeriesCollection? _chart;
        public SeriesCollection Chart
        {
            get => _chart ?? new();
            set => SetProperty(ref _chart, value);
        }

        private bool _priceChartVisible;
        public bool PriceChartVisible
        {
            get => _priceChartVisible;
            set
            {
                SetProperty(ref _priceChartVisible, value);
                NotChartVisibility = !value;
            }
        }

        private bool _notChartVisibility;
        public bool NotChartVisibility
        {
            get => _notChartVisibility;
            set
            {
                SetProperty(ref _notChartVisibility, value);
            }
        }

        private string _currentValue;
        public string CurrentValue
        {
            get => _currentValue;
            set => SetProperty(ref _currentValue, value);
        }

        private readonly int _currentValueFontSize;
        public int CurrentValueFontSize
        {
            get => _currentValueFontSize;
        }

        private readonly string _currentValueCaption;
        public string CurrentValueCaption
        {
            get => _currentValueCaption;
        }

        private ObservableCollection<AssetScoreModel> _assets;
        public ObservableCollection<AssetScoreModel> Assets
        {
            get => _assets;
            set => SetProperty(ref _assets, value);
        }

        private AssetScoreModel _selectedAsset;
        public AssetScoreModel SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                SetProperty(ref _selectedAsset, value);
                ChartFactory.RemoveAssetChart(PastSelectedAsset, Chart);
                PastSelectedAsset = ChartFactory.AddAssetChart(databaseManagement.CreateAssetComparableToMarket(_selectedAsset.Symbol), Chart);
            }
        }

        private LineSeries _pastSelectedAsset;
        public LineSeries PastSelectedAsset
        {
            get => _pastSelectedAsset;
            set => SetProperty(ref _pastSelectedAsset, value);
        }


        private ObservableCollection<PerformanceCardModel> _changes;
        public ObservableCollection<PerformanceCardModel> Changes
        {
            get => _changes;
            set => SetProperty(ref _changes, value);
        }

        private readonly RelayCommand _deleteCardCommand;
        public RelayCommand DeleteCardCommand
        {
            get => _deleteCardCommand;
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

        private int _cardWidth;
        public int CardWidth
        {
            get => _cardWidth;
            set => SetProperty(ref _cardWidth, value);
        }

        private readonly RelayCommand _switchCardWidth;
        public RelayCommand SwitchCardWidth
        {
            get => _switchCardWidth;
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

        private readonly RelayCommand _setTimeFrame9M;
        public RelayCommand SetTimeFrame9M
        {
            get => _setTimeFrame9M;
        }

        private readonly RelayCommand _setTimeFrame1Y;
        public RelayCommand SetTimeFrame1Y
        {
            get => _setTimeFrame1Y;
        }
        private readonly RelayCommand _setTimeFrame2Y;
        public RelayCommand SetTimeFrame2Y
        {
            get => _setTimeFrame2Y;
        }
        private readonly RelayCommand _setTimeFrame3Y;
        public RelayCommand SetTimeFrame3Y
        {
            get => _setTimeFrame3Y;
        }

        private readonly RelayCommand _setTimeFrame4Y;
        public RelayCommand SetTimeFrame4Y
        {
            get => _setTimeFrame4Y;
        }

        public MarketCardModel()
        {
            _name = "";
            _nameFontSize = 30;
            _currentValue = "";
            _currentValueFontSize = 35;
            _currentValueCaption = "Aktueller Wert:";
            _cardWidth = 500;
            _switchCardWidth = new RelayCommand(() => ChangeCardWidth());
            _chart = new SeriesCollection();
            _changes = new ObservableCollection<PerformanceCardModel>();
            _maxValues = new ObservableCollection<PerformanceCardModel>();
            _minValues = new ObservableCollection<PerformanceCardModel>();
            _assets = new();
            _selectedAsset = new();
            _pastSelectedAsset = new();
            _deleteCardCommand = new RelayCommand(DeleteCard);

            _setTimeFrame3M = new RelayCommand(() => SetTimeFrame(90));
            _setTimeFrame6M = new RelayCommand(() => SetTimeFrame(180));
            _setTimeFrame9M = new RelayCommand(() => SetTimeFrame(270));
            _setTimeFrame1Y = new RelayCommand(() => SetTimeFrame(365));
            _setTimeFrame2Y = new RelayCommand(() => SetTimeFrame(730));
            _setTimeFrame3Y = new RelayCommand(() => SetTimeFrame(1095));
            _setTimeFrame4Y = new RelayCommand(() => SetTimeFrame(1460));

            _maxXChartValue = DateTime.Now.Ticks;
            _minXChartValue = DateTime.Now.Subtract(new TimeSpan(180, 0, 0, 0)).Ticks;
            _separatorStep = TimeSpan.FromDays(30).Ticks;
            XFormatter = val => new DateTime((long)val).ToString("MMM yyyy");
            YFormatter = val => val.ToString("C");

            Messenger.Register<ChartVisibilityChangedMessage>(this, (r, m) =>
            {
                PriceChartVisible = m.Value;
            });
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

        private void DeleteCard()
        {
            Messenger.Send(new WatchlistDeleteMarketMessage(this));
        }

        private void ChangeCardWidth()
        {
            if (CardWidth == 500)
                CardWidth = 1100;
            else
                CardWidth = 500;

        }
    }
}
